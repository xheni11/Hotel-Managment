using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string HashOfPassword { get; set; }
        public string Username { get; set; }
        public string PhoneNr { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? DateCreated { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public int AccessFailedCount { get; set; }
        public List<RoleModel> UserRoles { get; set; }
        public virtual List<BookingModel> Bookings { get; set; }
        public virtual List<DriverServiceModel> DriverServices { get; set; }
        public virtual AnonymousRequestModel AnonymousRequest { get; set; }

    }
}
