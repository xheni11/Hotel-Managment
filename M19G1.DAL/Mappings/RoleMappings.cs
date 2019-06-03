using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mappings
{
    public static class RoleMappings
    {
        public static RoleModel MapRoleToRoleModel(AspNetRole role)
        {
            return new RoleModel
            {
                Id = role.Id,
                RoleName = role.Name,
                UserRequests = role.UserRequests.Select(ur => UserMappings.MapUserRequestToURModel(ur)).ToList(),
                Users = role.AspNetUsers.Select(u => UserMappings.MapAspNetUserToUserModel(u)).ToList()
            };

        }
    }
}
