using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class ExtraFacilityModel
    {
        public int Id { get; set; }
        public int EFQuantity { get; set; }
        public decimal Price { get; set; }
        public BookingRoomModel BookingRoom { get; set; }
        public FacilityModel Facility { get; set; }
        public int BookingRoomId { get; set; }
        public int FacilityId { get; set; }

    }
}
