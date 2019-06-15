using M19G1.Models;
using M19G1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.MappingViewModel
{
    public class AnonymousRequestViewModelMapping
    {
        public static AnonymousRequestViewModel ToViewModel(AnonymousRequestModel userModel)
        {
            return new AnonymousRequestViewModel
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                //Active = userModel.Active,
                Email = userModel.Email,
                Username = userModel.Username,
                //Birthday=(DateTime)userModel.Birthday,
                Gender=userModel.Gender
            };
        }

        public static AnonymousRequestModel ToModel(AnonymousRequestViewModel user)
        {
            return new AnonymousRequestModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                //Birthday = (DateTime)user.Birthday,
                //RoleName = user.RoleName,
                Username = user.Username,
                //Active = user.Active,
                Gender=user.Gender
                
                
            };
        }
        public static AnonymousRequestModel ToCreateUserModel(AnonymousRequestViewModel user)
        {
            return new AnonymousRequestModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                //Birthday = (DateTime)user.Birthday,
                //RoleName = user.RoleName,
                Username = user.Username,
                Gender = user.Gender
            
            };
        }
        public static List<AnonymousRequestViewModel> ToViewModel(IEnumerable<AnonymousRequestModel> user)
        {
            return user.Select(ToViewModel).ToList();
        }
    }
}