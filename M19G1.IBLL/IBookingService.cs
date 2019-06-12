using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.IBLL
{
    public interface IBookingService
    {
        List<BookingModel> GetOldBookings(int UserId);
        List<BookingModel> GetActiveBookings(int UserId);
        bool CancelBooking(int BookingId);
        BookingModel GetBookingById(int bookingId);
    }
}
