using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class UserRequestModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }

        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
}
