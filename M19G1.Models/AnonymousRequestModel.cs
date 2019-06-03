using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class AnonymousRequestModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Confirmed { get; set; }
        public UserModel User { get; set; }
        public int UserId { get; set; }
    }
}
