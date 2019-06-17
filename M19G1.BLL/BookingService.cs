using M19G1.DAL;
using M19G1.DAL.Entities;
using M19G1.DAL.Mappings;
using M19G1.IBLL;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.BLL
{
    public class BookingService : IBookingService
    {
        private readonly UnitOfWork _internalUnitOfWork;
        public BookingService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }

        public List<BookingModel> GetOldBookings(int UserId)
        { 
            DateTime now = DateTime.Now;
            List<BookingModel> oldBookings = _internalUnitOfWork.BookingsRepository.Get(b => b.Valid == true && b.UserId == UserId && b.CheckOutTime <= now)
                .Select(b => BookingMappings.MapBookingToBookingModel(b,null)).ToList();
            return oldBookings;
            
        }
        public List<BookingModel> GetActiveBookings(int UserId)
        {
            DateTime now = DateTime.Now;
            List<BookingModel> activeBookings = _internalUnitOfWork.BookingsRepository.Get(b => b.Valid == true && b.UserId == UserId && b.CheckOutTime == null && b.EndDate >= now)
                .Select(m => BookingMappings.MapBookingToBookingModel(m,null)).ToList();
            foreach(var booking in activeBookings)
            {
                booking.Cancelable = isCancelable(booking);
            }
            return activeBookings;
        }

        private bool isCancelable(BookingModel booking)
        {
            if (booking.BookTime <= booking.Start.AddHours(-24))
            {
                // booking i kryer te pakten 24 ore para fillimit
                if (DateTime.Now <= booking.BookTime.AddHours(4))
                {
                    //jemi brenda afatit 4 ore per ta bere cancel
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool CancelBooking(int bookingId)
        {
            Booking booking = _internalUnitOfWork.BookingsRepository.GetByID(bookingId);
            BookingModel bookingModel = BookingMappings.MapBookingToBookingModel(booking,null);
            if (!isCancelable(bookingModel))
                return false;
            else
            {
                booking.Valid = false;
                _internalUnitOfWork.BookingsRepository.Update(booking);
                _internalUnitOfWork.Save();
                return true;
            }
        }

        public BookingModel GetBookingById(int bookingId)
        {
            Booking booking = _internalUnitOfWork.BookingsRepository.GetByID(bookingId);
            BookingModel bookingModel = BookingMappings.MapBookingToBookingModel(booking, null);
            return bookingModel;
        }

        public int CreateNewBooking(BookingModel model)
        {
            model.Valid = false; // ende nuk kemi shtuar dhomat
            Booking booking = BookingMappings.MapBookingModelToBooking(model);
            _internalUnitOfWork.BookingsRepository.Insert(booking);
            _internalUnitOfWork.Save();
            int id = booking.Id;
            return id;
        }

        public bool AddRoomForBooking(ChooseRoomModel model)
        {
            if(_internalUnitOfWork.RoomRepository.GetByID(model.roomId).Occupied == false)
            {
                BookingRoom bookingRoom = BookingMappings.MapChooseRoomModelToBookingRoom(model);
                _internalUnitOfWork.BookingRoomRepository.Insert(bookingRoom);
                _internalUnitOfWork.Save();
                int bookingRoomId = bookingRoom.Id;
                foreach(int facId in model.facilityIds)
                {
                    _internalUnitOfWork.ExtraFacilityRepository.Insert(
                        new ExtraFacility()
                        {
                            BookingRoomId = bookingRoomId,
                            FacilityId = facId,
                            Price = 0.0M,
                            Quantity = 1
                        });
                    _internalUnitOfWork.Save();
                }
                return true;
            }
            return false;
        }

        public bool FinishBooking(int bookingId)
        {
            Booking booking = _internalUnitOfWork.BookingsRepository.GetByID(bookingId);
            if(booking != null)
            {
                booking.Valid = true;
                _internalUnitOfWork.BookingsRepository.Update(booking);
                _internalUnitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
