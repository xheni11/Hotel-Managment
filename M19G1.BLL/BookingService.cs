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
            return _internalUnitOfWork.BookingsRepository.Get(b => b.Valid == true && b.UserId == UserId && b.EndDate <= now)
                .Select(b => BookingMappings.MapBookingToBookingModel(b)).ToList();
        }
        public List<BookingModel> GetNewBookings(int UserId)
        {
            DateTime now = DateTime.Now;
            return _internalUnitOfWork.BookingsRepository.Get(b => b.Valid == true && b.UserId == UserId && b.EndDate >= now)
                .Select(b => BookingMappings.MapBookingToBookingModel(b)).ToList();
        }


    }
}
