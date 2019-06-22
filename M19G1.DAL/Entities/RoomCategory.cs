using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public  class RoomCategory:BaseEntity
    {
        public RoomCategory()
        {
            Rooms = new HashSet<Room>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
