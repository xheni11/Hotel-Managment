using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class ChooseRoomViewModel
    {
        public BookingModel booking { get; set; }

        [Display(Name = "Room ")]
        [Required(ErrorMessage = "Room should be specified !")]
        public int RoomId { get; set; }
        public List<RoomModel> rooms { get; set; }

        [Display(Name = "Extra facilities ")]
        public int[] facilityIds { get; set; }
        public List<FacilityModel> facilities { get; set; }
    }
}