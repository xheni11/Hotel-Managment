using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Mappings
{
    public class RatingMappings
    {
        public static RatingViewModel MapRatingToRatingViewModel(RatingModel model)
        {
            return new RatingViewModel
            {
                Id = model.Id,
                BookingId = model.BookingId,
                Description = model.Description,
                Rate = model.RateValue
            };

        }

        public static RatingModel MapRatingViewModelToRatingModel(RatingViewModel model)
        {
            return new RatingModel
            {
                RateValue = model.Rate,
                Description = model.Description,
                BookingId = model.BookingId,
                DateCreated = DateTime.Now
            };

        }
    }
}