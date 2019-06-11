﻿using System;
using System.Collections.Generic;

namespace M19G1.DAL.Entities
{
    public partial class Booking
    { 
        public Booking()
        {
            BookingRooms = new HashSet<BookingRoom>();
            DriverServices = new HashSet<PersonalDriverService>();
        }
        public int Id { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Valid { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public int UserId { get; set; }
        public virtual AspNetUser Client { get; set; }
        public virtual ICollection<BookingRoom> BookingRooms { get; set; }
        public virtual ICollection<PersonalDriverService> DriverServices { get; set; }

        public virtual Rating Rating { get; set; }
    }
}
