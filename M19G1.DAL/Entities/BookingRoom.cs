using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public class BookingRoom:BaseEntity
    {
        public BookingRoom()
        {
            ExtraFacilities = new HashSet<ExtraFacility>();
        }
        public int Id { get; set; }
        public virtual Room Room { get; set; }
        public virtual Booking Booking { get; set; }
        public int RoomId { get; set; }
        public int BookingId { get; set; }

        public virtual ICollection<ExtraFacility> ExtraFacilities { get; set; }
    }
}
