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
                RoleName = userModel.RoleName,
                Username = userModel.Username,
                Birthday = (DateTime)userModel.Birthday,
                Gender = userModel.Gender
            };
        }
        public static UserRequestModel ToCreateViewModel(UserRequestViewModel user)
        {
            return new UserRequestModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = (DateTime)user.Birthday,
                RoleName = user.RoleName,
                Username = user.Username,
                Gender = user.Gender

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
                RoleName = user.RoleName,
                Username = user.Username,
                Gender = user.Gender


            };
        }
        public static List<UserRequestViewModel> ToViewModel(IEnumerable<UserRequestModel> user)
        {
            return user.Select(ToViewModel).ToList();
        }
    }
}