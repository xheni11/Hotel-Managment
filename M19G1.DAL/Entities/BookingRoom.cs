

namespace M19G1.DAL.Entities
{
    using System.Collections.Generic;
    public partial class BookingRoom
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
