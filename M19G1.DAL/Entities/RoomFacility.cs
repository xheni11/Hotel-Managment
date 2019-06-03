using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{

    public  class RoomFacility:BaseEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Room Room { get; set; }
        public Facility Facility { get; set; }
        public int RoomId { get; set; }
        public int FacilityId { get; set; }
    }
}
