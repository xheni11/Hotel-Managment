using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public class ExtraFacility:BaseEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public BookingRoom BookingRoom { get; set; }
        public Facility Facility { get; set; }
        public int BookingRoomId { get; set; }
        public int FacilityId { get; set; }
    }
}
