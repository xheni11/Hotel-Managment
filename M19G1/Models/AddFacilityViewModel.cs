using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class AddFacilityViewModel
    {
        public BookingViewModel Booking { get; set; }

        [Display(Name = "Room Name ")]
        [Required(ErrorMessage = "A room should be specified !")]
        public int RoomId { get; set; }

        [Display(Name ="Facility")]
        [Required(ErrorMessage ="A facility should be specified !")]
        public int FacilityId { get; set; }
        public List<FacilityModel> Facilities { get; set; }

        [Display(Name ="Quantity")]
        [Range(1,5,ErrorMessage ="Quantity must be between 1 and 5")]
        public int FacilityQuantity { get; set; }

    }
}