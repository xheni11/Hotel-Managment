using M19G1.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M19G1.Models;
using M19G1.DAL;
using M19G1.DAL.Mappings;
using M19G1.DAL.Entities;

namespace M19G1.BLL
{
    public class RatingService : IRatingService
    {
        private readonly UnitOfWork _internalUnitOfWork;
        public RatingService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }
        public bool AddBookingRating(RatingModel model)
        {
            if(model.RateValue >= 0 && model.RateValue <=5)
            {              
                Rating rating = BookingMappings.MapRatingModelToRating(model,null);
                rating.Booking = _internalUnitOfWork.BookingsRepository.GetByID(model.BookingId);
                _internalUnitOfWork.RatingRepository.Insert(rating);
                _internalUnitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
