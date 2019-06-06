using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class FilterRoomViewModel
    {
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<RoomCategoryModel> Categories { get; set; }

        [Display(Name ="Room Name")]
        public string Name { get; set; }

        [Display(Name = "Max Price")]
        [Range(0.0,1000.0,ErrorMessage = "Price must be between 0 and 1000")]
        public decimal Price { get; set; }

        [Display(Name ="Occupied")]
        public bool Occupied { get; set; }

        [Display(Name ="Facility List")]
        public int[] SelectedFacilities { get; set; }
        public List<FacilityModel> Facilities { get; set; }

        public List<RoomModel> Rooms { get; set; }
    }
}