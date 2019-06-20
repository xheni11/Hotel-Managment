using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.MappingViewModel
{
    public static class LoginViewModelToModel
    {
        public static UserModel toModel (LoginViewModel loginViewModel)
        {
            return new UserModel
            {
                Username=loginViewModel.Username,
                

            };
        }
    }
}