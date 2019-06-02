
using System.Collections.Generic;

namespace M19G1.DAL.Entities
{
    public partial class Facility
    {
        public Facility()
        {
            RoomFacilities = new HashSet<RoomFacility>();
            ExtraFacilities = new HashSet<ExtraFacility>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public virtual ICollection<RoomFacility> RoomFacilities { get; set; }
        public virtual ICollection<ExtraFacility> ExtraFacilities { get; set; }
    }
}
