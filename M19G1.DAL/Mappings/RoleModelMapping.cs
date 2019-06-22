using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mapping.Role
{
    public class RoleModelMapping
    {
        public static RoleModel ToModel(AspNetRole role)
        {
            return new RoleModel
            {
                Id = role.Id,
                RoleName=role.Name,

            };
        }


        public static List<RoleModel> ToModel(IEnumerable<AspNetRole> role)
        {
            return role.Select(ToModel).ToList();
        }
    }
}
