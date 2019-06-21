using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class FilterRoomModel
    {
        public int CategoryId { get; set; }
        public string RoomName { get; set; }
        public decimal LessThanPrice { get; set; }
        public bool Occupied { get; set; }
        public int[] SelectedFacilities { get; set; }
    }
}
