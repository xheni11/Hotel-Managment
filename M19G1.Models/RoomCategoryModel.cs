using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class RoomCategoryModel
    {
        public int Id { get; set; }
        public string CatName { get; set; }
        public string Description { get; set; }
        public List<RoomModel> Rooms { get; set; }
    }
}
