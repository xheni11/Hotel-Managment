using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class DriverServiceModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public bool Completed { get; set; }
        public BookingModel Booking { get; set; }
        public int BookingId { get; set; }
        public UserModel Driver { get; set; }
        public int? DriverId { get; set; }
        public TimeSpan? TotalTime { get; set; }
    }
}
