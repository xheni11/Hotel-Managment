using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class RoomFacilityModel
    {
        public int Id { get; set; }
        public int FQuantity { get; set; }
        public RoomModel Room { get; set; }
        public FacilityModel Facility { get; set; }
        public int RoomId { get; set; }
        public int FacilityId { get; set; }

    }
}
