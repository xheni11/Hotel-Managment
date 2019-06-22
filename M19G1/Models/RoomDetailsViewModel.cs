using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class RoomDetailsViewModel
    {
        [Display(Name ="Room Name")]
        public string RoomName { get; set; }
        [Display(Name = "Number of Guests")]
        public int GuestsNr { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Description")]
        public string RoomDescription { get; set; }
        public RoomCategoryModel RoomCategory { get; set; }
        public List<RoomFacilityModel> RoomFacilities { get; set; }
    }
}