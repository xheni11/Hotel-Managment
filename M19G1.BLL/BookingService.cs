using M19G1.DAL;
using M19G1.IBLL;
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

    }
}
