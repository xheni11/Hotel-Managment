using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class FacilityModel
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public List<RoomFacilityModel> RoomFacilities { get; set; }
        public List<ExtraFacilityModel> ExtraFacilites { get; set; }

    }
}
