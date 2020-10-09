using M19G1.DAL.Entities;
using M19G1.DAL.Repository;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace M19G1.DAL.Mapping
{
    public class UserRequestModelMapping
    {
        public static UserRequestModel ToModel(UserRequest user)
        {

            return new UserRequestModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = user.Birthday,
                Username = user.Username,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                RoleId=user.RoleId,
                RoleName = user.Role.Name
            };
        }

        public static UserRequest ToEntityToCreate(UserRequestModel user)
        {

            return new UserRequest
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = (DateTime)user.Birthday,
                Username = user.Username,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                CreatedOn = DateTime.Now,
                Deleted = false,
                CreatedBy = 1,
                RoleId=user.RoleId
                
                
            };
        }
        public static List<UserRequestModel> ToModel(IEnumerable<UserRequest> user)
        {
            return user.Select(ToModel).ToList();
        }
    }
}
