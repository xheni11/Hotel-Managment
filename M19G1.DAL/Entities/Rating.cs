using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public partial class Rating
    {
        public int Id { get; set; }
        [Range(0.0,5.0)]
        public double Rate { get; set; }
        public string Description { get; set; }
        public virtual Booking Booking { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
