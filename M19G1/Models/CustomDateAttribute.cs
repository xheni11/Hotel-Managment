using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute() : base(typeof(DateTime), DateTime.Now.ToShortDateString(),
            DateTime.Now.AddDays(10).ToShortDateString())
        { }
    }
}