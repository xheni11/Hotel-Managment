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

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Check In Time")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? CheckInTime { get; set; }

        [Display(Name = "Check Out Time")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? CheckOutTime {get;set;}

        [Display(Name ="Driver Services")]
        public List<DriverServiceViewModel> DriverServices { get; set; }
        public List<RoomViewModel> Rooms { get; set; }

    }

        
}