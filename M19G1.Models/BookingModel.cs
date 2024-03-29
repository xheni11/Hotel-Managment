﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public  class BookingModel
    {
        public int Id { get; set; }
        public DateTime BookTime { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Valid { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int ClientId { get; set; }
        public UserModel Client { get; set; }
        public List<BookingRoomModel> BookingRooms { get; set; }
        public List<DriverServiceModel> DriverServices { get; set; }
        public RatingModel Rating { get; set; }

        public bool Cancelable { get; set; }
        
    }
}
