using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mappings
{
    public static class UserMappings
    {
        public static UserModel MapAspNetUserToUserModel(AspNetUser user)
        {
            var userModel =  new UserModel
            {
                Id = user.Id,
                FName = user.FirstName,
                LName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                AccessFailedCount = user.AccessFailedCount,
                Active = user.Enabled,
                Birthday = user.Birthday,
                DateCreated = user.DateCreated,
                HashOfPassword = user.PasswordHash,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                PhoneNr = user.PhoneNumber,
                Username = user.UserName,
                SecurityStamp = user.SecurityStamp,
                TwoFactorEnabled = user.TwoFactorEnabled,
                AnonymousRequest = MapAnonymousRequestToARModel(user.AnonymousRequest)
            };
            userModel.Bookings = user.Bookings.Select(b => BookingMappings.MapBookingToBookingModel(b, userModel)).ToList();
            userModel.DriverServices = user.DriverServices.Select(ds => DriverServiceMappings.MapDriverServiceToDriverServiceModel(ds,null, userModel)).ToList();
            userModel.UserRoles = user.AspNetRoles.Select(r => RoleMappings.MapRoleToRoleModel(r)).ToList();
            return userModel;
        }
        public static AnonymousRequestModel MapAnonymousRequestToARModel(AnonymousRequest request)
        {
            return new AnonymousRequestModel
            {
                Id = request.Id,
                UserId = request.User.Id,
                User = MapAspNetUserToUserModel(request.User),
                Confirmed = request.Confirmed,
                DateCreated = request.DateCreated
                 
            };
        }

        public static UserRequestModel MapUserRequestToURModel(UserRequest request)
        {
            return new UserRequestModel
            {
                Birthday = request.Birthday,
                Email = request.Email,
                EmailConfirmed = request.EmailConfirmed,
                FName = request.FirstName,
                LName = request.LastName,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                RequestId = request.Id,
                RoleId = request.RoleId,
                Role = RoleMappings.MapRoleToRoleModel(request.Role)
            };

        }

    }
}
