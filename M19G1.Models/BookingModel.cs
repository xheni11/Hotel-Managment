using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public  class BookingModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Valid { get; set; }
        public bool CheckIn { get; set; }
        public bool CheckOut { get; set; }
        public int ClientId { get; set; }
        public UserModel Client { get; set; }
        public List<BookingRoomModel> BookingRooms { get; set; }
        public List<TaxiServiceModel> DriverServices { get; set; }
        
    }
}
