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
                RoleName = userModel.RoleName,
                Username = userModel.Username,
                Birthday=(DateTime)userModel.Birthday,
                Gender=userModel.Gender
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
                Birthday = (DateTime)user.Birthday,
                RoleName = user.RoleName,
                Username = user.Username,
                Active = user.Active,
                Gender=user.Gender
                
                
            };
        }
        public static UserModel ToCreateUserModel(UserViewModel user)
        {
            return new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = (DateTime)user.Birthday,
                RoleName = user.RoleName,
                Username = user.Username,
                Active = user.Active,
                Gender = user.Gender
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