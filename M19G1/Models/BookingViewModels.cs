using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace M19G1.Models
{
    public class BookingElementViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "List of rooms")]
        public string RoomList { get; set; }
        [Display(Name = "Rating")]
        public double rate { get; set; }

        public bool Cancelable { get; set; }
    }

    public class BookingViewModel
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public List<DriverServiceViewModel> DriverServices { get; set; }
        public List<RoomViewModel> Rooms { get; set; }

    }

        
}