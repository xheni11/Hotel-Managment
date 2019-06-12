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
        public int RoomId { get; set; }

        [Display(Name ="Facility")]
        public int FacilityId { get; set; }
        public List<FacilityModel> Facilities { get; set; }

        public int FacilityQuantity { get; set; }

        public DateTime PickUpTime { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }

    }
}