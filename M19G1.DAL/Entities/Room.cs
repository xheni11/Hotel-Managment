using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public  class Room:BaseEntity
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
        public decimal Vacancy { get; set; }
        public bool Occupied { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public RoomCategory Category { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<BookingRoom> BookingRooms { get; set; }
        public virtual ICollection<RoomFacility> RoomFacilities { get; set; }
    }
}
