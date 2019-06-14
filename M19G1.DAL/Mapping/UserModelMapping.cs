using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mapping.User
{
    public class UserModelMapping
    {

        public static UserModel ToModel(AspNetUser user)
        {
            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Active = user.Enabled,
                Birthday = user.Birthday,
                Gender = user.Gender,
                DateCreated = user.DateCreated,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                PhoneNr = user.PhoneNumber,
                Username = user.UserName,
                //RoleName= user.AspNetRoles.FirstOrDefault().Name
            };
        }


        public static AspNetUser ToEntity(UserModel userModel, AspNetUser userToUpdate)
        {

            userToUpdate.FirstName = userModel.FirstName;
            userToUpdate.LastName = userModel.LastName;
            userToUpdate.UserName = userModel.Username;
            userToUpdate.Birthday = userModel.Birthday;
            userToUpdate.Gender = userModel.Gender;
            userToUpdate.Email = userModel.Email;        
            userToUpdate.PhoneNumber = userModel.PhoneNr;

            return userToUpdate;
        }
        public static AspNetUser ToEntityToAnonymous( AspNetUser userToUpdate)
        {
            string anonym = "xxxxx";
            userToUpdate.FirstName =anonym;
            userToUpdate.LastName = anonym;
            userToUpdate.UserName = anonym;
            userToUpdate.Birthday = DateTime.Now;
            userToUpdate.Gender ="x";
            userToUpdate.Email = anonym;
            userToUpdate.PhoneNumber= anonym;
            userToUpdate.PasswordHash = anonym;

            return userToUpdate;
        }
        public static AspNetUser ToEntityToCreate(UserModel userModel,string hashedPassword)
        {
            return new AspNetUser
            {
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                Birthday = userModel.Birthday,
                Deleted = false,
                DateCreated = DateTime.Now,
                Email = userModel.Email,
                Enabled = true,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                UserName = userModel.Username,
                Gender = userModel.Gender,
                PhoneNumber = userModel.PhoneNr,
                PasswordHash = hashedPassword
            };
        }
        public static AspNetUser ToEntityToCreate(UserRequestModel userModel, string hashedPassword)
        {
            return new AspNetUser
            {
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                Birthday = userModel.Birthday,
                Deleted = false,
                DateCreated = DateTime.Now,
                Email = userModel.Email,
                Enabled = true,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                UserName = userModel.Username,
                Gender = userModel.Gender,
                PhoneNumber = userModel.PhoneNumber,
                PasswordHash = hashedPassword
            };
        }
        public static List<UserModel> ToModel(IEnumerable<AspNetUser> user)
        {
            return user.Select(ToModel).ToList();
        }
    }
}
