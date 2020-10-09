using M19G1.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.MappingViewModel
{
    public static class UserViewModelMapping
    {
        public static UserViewModel ToViewModel(UserModel userModel)
        {
            return new UserViewModel
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Active = userModel.Active,
                Email = userModel.Email,
                UserName = userModel.UserName,
                Enabled=userModel.Enabled,
                Roles=userModel.UserRoles.Select(r=>r.RoleName).ToList()
            };
        }

        public static UserModel ToModel(UserViewModel user)
        {
            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Active = user.Active,
                Enabled=user.Enabled
                
                
            };
        }
        public static EmailUserViewModel ToEmailUserViewModel(AddUserViewModel user)
        {
            return new EmailUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                UserName = user.UserName,
                Email = user.Email
            };
        }
        public static EmailUserViewModel ToEmailUserViewModel(UserModel user)
        {
            return new EmailUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                UserName = user.UserName,
                Email = user.Email
            };
        }
        public static UserModel ToCreateUserModel(AddUserViewModel user)
        {
            return new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = (DateTime)user.Birthday,
                UserName = user.UserName,
                Gender = user.Gender,
                RoleId=user.RoleId,
                Id=user.Id
            };
        }

        public static AddUserViewModel ToModel(UserModel user)
        {
            return new AddUserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = (DateTime)user.Birthday,
                UserName = user.UserName,
                Gender = user.Gender,
                RoleId = user.RoleId,
                Id=user.Id,
                Roles=user.UserRoles
            };
        }
        public static List<UserViewModel> ToViewModel(IEnumerable<UserModel> user)
        {
            return user.Select(ToViewModel).ToList();
        }
        public static UserClientModel ToCreateClientModel(UserClientViewModel user)
        {
            return new UserClientModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = (DateTime)user.Birthday,
                RoleName = "Client",
                Username = user.Username,
                Active = true,
                Gender = user.Gender
            
                
            };
        }
       
    }
}