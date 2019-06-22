using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class RatingModel
    {
        public int Id { get; set; }
        [Range(0.0,5.0)]
        public double RateValue { get; set; }
        public string Description { get; set; }
        public BookingModel Booking { get; set; }
        public int BookingId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
