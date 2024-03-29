﻿using M19G1.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M19G1.Models;
using M19G1.DAL;
using M19G1.DAL.Entities;
using System.Linq.Expressions;
using M19G1.DAL.Mappings;

namespace M19G1.BLL
{
    class SwapVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public SwapVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }

    public class RoomService : IRoomService
    {
        private readonly UnitOfWork _internalUnitOfWork;

        public RoomService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }

        public List<RoomModel> FilterRooms(FilterRoomModel model)
        {


            Expression<Func<Room, bool>> expr1 = r => r.Occupied == model.Occupied && r.Enabled == true;
            Expression<Func<Room, bool>> expr = expr1;
            if (model.CategoryId != 0)
            {
                Expression<Func<Room, bool>> expr2 = r => r.CategoryId == model.CategoryId;
                expr = Expression.Lambda<Func<Room, bool>>(Expression.AndAlso(
                new SwapVisitor(expr.Parameters[0], expr2.Parameters[0]).Visit(expr.Body),
                expr2.Body), expr2.Parameters);


            }
            if (model.RoomName != null)
            {
                Expression<Func<Room, bool>> expr3 = r => r.Name == model.RoomName;

                expr = Expression.Lambda<Func<Room, bool>>(Expression.AndAlso(
            new SwapVisitor(expr.Parameters[0], expr3.Parameters[0]).Visit(expr.Body),
            expr3.Body), expr3.Parameters);

            }
            if (model.LessThanPrice > 0)
            {
                Expression<Func<Room, bool>> expr4 = r => r.Price <= model.LessThanPrice;
                expr = Expression.Lambda<Func<Room, bool>>(Expression.AndAlso(
            new SwapVisitor(expr.Parameters[0], expr4.Parameters[0]).Visit(expr.Body),
            expr4.Body), expr4.Parameters);
            }


            List<RoomModel> rooms = _internalUnitOfWork.RoomRepository.Get(expr).Select(m =>
                 RoomMappings.MapRoomToRoomModel(m, RoomMappings.MapRoomCategoryToRCModel(m.Category))).ToList();
            List<RoomModel> filteredRooms = new List<RoomModel>();

            if (model.SelectedFacilities != null)
            {
                bool addFlag = true;
                foreach (var room in rooms)
                {
                    addFlag = true;
                    foreach (int id in model.SelectedFacilities)
                    {
                        if (room.RoomFacilities.Where(rf => rf.FacilityId == id).Count() == 0)
                        {
                            addFlag = false;
                            break;
                        }
                    }
                    if (addFlag)
                        filteredRooms.Add(room);
                }
            }
            else
            {
                filteredRooms = rooms;
            }
            return filteredRooms;
        }

        public List<RoomModel> GetFreeRoomsForBooking(int bookingId)
        {
            List<int> NotAllowedRooms = new List<int>();
            Booking booking = _internalUnitOfWork.BookingsRepository.GetByID(bookingId);
            List<int> bookingRooms = booking.BookingRooms?.Select(br => br.Room.Id).ToList();
            if (bookingRooms != null)
                NotAllowedRooms.AddRange(bookingRooms);
            List<Booking> bookingsWithIntersect = _internalUnitOfWork.BookingsRepository.Get(b => b.Valid &&
            ((b.StartDate < booking.StartDate && b.EndDate < booking.StartDate) || (b.StartDate > booking.EndDate && b.EndDate > booking.EndDate))).ToList();

            if(bookingRooms != null)
            {
                if(bookingRooms.Count != 0)
                {
                    foreach(var book in bookingsWithIntersect)
                    {
                        if(book.BookingRooms != null)
                        {
                            NotAllowedRooms.AddRange(book.BookingRooms.Select(br => br.RoomId).ToList());
                        }
                    }
                }
            }
            NotAllowedRooms = NotAllowedRooms.Distinct().ToList();
            List<RoomModel> allowedRooms = _internalUnitOfWork.RoomRepository.Get(r => !NotAllowedRooms.Contains(r.Id))
                .Select(r => RoomMappings.MapRoomToRoomModel(r, null)).ToList();
            if (allowedRooms == null)
                return new List<RoomModel>();
            else
                return allowedRooms;
        }

        public RoomModel GetRoomById(int id)
        {
            Room room = _internalUnitOfWork.RoomRepository.GetByID(id);
            if (room == null)
                return null;
            RoomCategoryModel roomCategory = new RoomCategoryModel();
            if(room.Category != null)
            {
                roomCategory = RoomMappings.MapRoomCategoryToRoomCategoryModel(room.Category);
            }
            if (roomCategory.CatName == null)
                roomCategory = null;
            RoomModel roomModel = RoomMappings.MapRoomToRoomModelDetails(room, roomCategory );
            return roomModel;
        }

        private bool Intersect(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            bool notIntersect = (start1 < start2 && end1 < start2) || (start1 > end2 && end1 > end2);
            return !notIntersect;
        }
    }
}
