using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public int GuestsNr { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public bool Occupied { get; set; }
        public string RoomDescription { get; set; }
        public RoomCategoryModel RoomCategory { get; set; }
        public int CategoryId { get; set; }
        public List<BookingRoomModel> BookingRooms { get; set; }
        public List<RoomFacilityModel> RoomFacilities { get; set; }

    }
}
