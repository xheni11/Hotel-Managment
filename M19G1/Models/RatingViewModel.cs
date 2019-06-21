using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage ="Rate value should be specified !")]
        [Range(0.0,5.0,ErrorMessage ="Rate value is between 0 and 5")]
        public double Rate { get; set; }
    }
}