using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class CreateBookingViewModel
    {
        [Display(Name = "Start date ")]
        [BookingDate(ErrorMessage = "Booking start date should be between next 2 hours and 30 days")]
        [Required(ErrorMessage ="Start date should be specified !")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date ")]
        [Required(ErrorMessage ="End date should be specified !")]
        public DateTime EndDate { get; set; }

        [Display(Name ="Initial pick up location ")]
        public string InitialLocation { get; set; }
        [Display(Name ="Final drop off location ")]
        public string FinalLocation { get; set; }
    }
}