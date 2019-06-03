using M19G1.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.IBLL
{
    public interface IBookingService
    {
        List<Booking> GetOldBookings(int UserId);
        List<Booking> GetNewBookings(int UserId);
    }
}
