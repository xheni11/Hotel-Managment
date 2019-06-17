using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class ChooseRoomModel
    {
        public int bookingId { get; set; }
        public int roomId { get; set; }
        public int[] facilityIds { get; set; }
    }
}
