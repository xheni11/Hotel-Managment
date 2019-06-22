using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public class PersonalDriverService:BaseEntity
    {
        public int Id { get; set; }
        public DateTime PickUpTime { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public bool Completed { get; set; }
        public Booking Booking { get; set; }
        public int BookingId { get; set; }
        public AspNetUser Driver { get; set; }
        public int? DriverID { get; set; }
        public TimeSpan VoyageTime { get; set; }
        public bool Taken { get; set; }
    }
}
