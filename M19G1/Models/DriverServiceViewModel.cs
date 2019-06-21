using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class DriverServiceViewModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public bool Completed { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}