using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public class Booking:BaseEntity
    {
        public Booking()
        {
            BookingRooms = new HashSet<BookingRoom>();
            DriverServices = new HashSet<PersonalDriverService>();
        }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Valid { get; set; }
        public bool CheckIn { get; set; }
        public bool CheckOut { get; set; }
        public int UserId { get; set; }
        public AspNetUser Client { get; set; }
        public virtual ICollection<BookingRoom> BookingRooms { get; set; }
        public virtual ICollection<PersonalDriverService> DriverServices { get; set; }

        public virtual Rating Rating { get; set; }

    }
}
