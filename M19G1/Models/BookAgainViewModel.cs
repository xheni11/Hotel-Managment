using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class BookAgainViewModel
    {
        public int bookingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}