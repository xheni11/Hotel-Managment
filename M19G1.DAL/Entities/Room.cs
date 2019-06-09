
using System.Collections.Generic;

namespace M19G1.DAL.Entities
{
    public partial class Room
    {
        public Room()
        {
            BookingRooms = new HashSet<BookingRoom>();
            RoomFacilities = new HashSet<RoomFacility>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int NrOfGuests { get; set; }
        public decimal Price { get; set; }
        public bool Enabled { get; set; }
        public bool Occupied { get; set; }
        public string Description { get; set; }
        public virtual RoomCategory Category { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<BookingRoom> BookingRooms { get; set; }
        public virtual ICollection<RoomFacility> RoomFacilities { get; set; }
    }
}
