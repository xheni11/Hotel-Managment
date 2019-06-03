using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class UserRequestModel
    {
        public int RequestId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public RoleModel Role { get; set; }
        public int RoleId { get; set; }
    }
}
