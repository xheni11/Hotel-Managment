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
        public string Active { get; set; }
        public string Email { get; set; }
        public string HashOfPassword { get; set; }
        public string Username { get; set; }
        public string PhoneNr { get; set; }
        public string UserName { get; set; }
    }
}
