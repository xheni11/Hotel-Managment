using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class BookingRoomModel
    {
        public int Id { get; set; }
        public RoomModel Room { get; set; }
        public int RoomId { get; set; }
        public BookingModel BookingModel { get; set; }
        public int BookingId { get; set; }

        public List<ExtraFacilityModel> ExtraFacilities { get; set; }
    }
}
