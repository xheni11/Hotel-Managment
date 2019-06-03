using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<UserModel> Users { get; set; }
        public List<UserRequestModel> UserRequests { get; set; }

    }
}
