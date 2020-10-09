using M19G1.Models;
using M19G1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.MappingViewModel
{
    public class UserRequestViewModelMapping
    {
        public static UserRequestViewModel ToViewModel(UserRequestModel userModel)
        {
            return new UserRequestViewModel
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Username = userModel.Username,
                Birthday = (DateTime)userModel.Birthday,
                Gender = userModel.Gender,
                RoleId=userModel.RoleId,
                RoleName=userModel.RoleName
            };
        }
        public static UserModel ToCreateViewModel(UserRequestViewModel user)
        {
            return new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = (DateTime)user.Birthday,
                UserName = user.Username,
                Gender = user.Gender,
                RoleId=user.RoleId
                

            };
        }
        public static UserRequestModel ToCreateRequestViewModel(UserRequestViewModel user)
        {
            return new UserRequestModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = (DateTime)user.Birthday,
                Username = user.Username,
                Gender = user.Gender,
                RoleId=user.RoleId


            };
        }
        public static UserRequestModel ToModel(UserRequestViewModel user)
        {
            return new UserRequestModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = (DateTime)user.Birthday,
                Username = user.Username,
                Gender = user.Gender,
                RoleId=user.RoleId


            };
        }
        public static List<UserRequestViewModel> ToViewModel(IEnumerable<UserRequestModel> user)
        {
            return user.Select(ToViewModel).ToList();
        }
    }
}