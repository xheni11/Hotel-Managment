using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class BookingDateAttribute : RangeAttribute
    {
        public BookingDateAttribute() : base(typeof(DateTime), DateTime.Now.AddHours(2).ToString(),
            DateTime.Now.AddDays(30).ToString())
        { }
    }
}