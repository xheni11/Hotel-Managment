
using System;

namespace M19G1.DAL.Entities
{
    public partial class PersonalDriverService
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
        

    }
}
