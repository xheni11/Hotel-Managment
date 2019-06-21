using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class AddDriverServiceViewModel
    {
        public BookingViewModel Booking { get; set; }

        [Display(Name = "Pick up time ")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Pick up time should be specified !")]
        [CustomDate(ErrorMessage ="Pick up time should be between next 10 minutes and 10 days !")]
        public DateTime PickUpTime { get; set; }
        [Required(ErrorMessage = "Location should be specified !")]
        public string Location { get; set; }
        public string Destination { get; set; }
    }
}