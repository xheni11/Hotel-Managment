﻿using M19G1.DAL.Entities;
using M19G1.DAL.Mapping.Role;
using M19G1.Models;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mappings
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
                UserName = user.UserName,
                RoleId = user.AspNetRoles.FirstOrDefault().Id,
                UserRoles= user.AspNetRoles.Select(RoleModelMapping.ToModel).ToList(),
                Enabled=user.Enabled
            };
        }


        public static AspNetUser ToEntity(UserModel userModel, AspNetUser userToUpdate)
        {

            userToUpdate.FirstName = userModel.FirstName;
            userToUpdate.LastName = userModel.LastName;
            userToUpdate.UserName = userModel.UserName;
            userToUpdate.Birthday = userModel.Birthday;
            userToUpdate.Gender = userModel.Gender;
            userToUpdate.Email = userModel.Email;
            userToUpdate.PhoneNumber = userModel.PhoneNr;
            userToUpdate.Enabled = userModel.Enabled;
            //userToUpdate.AspNetRoles=userModel.RoleName
            return userToUpdate;
        }

        public static AspNetUser ToEntityToAnonymous(AspNetUser userToUpdate)
        {
            string anonym = "xxxxx";
            userToUpdate.FirstName = anonym;
            userToUpdate.LastName = anonym;
            userToUpdate.UserName = anonym;
            userToUpdate.Birthday = DateTime.Now;
            userToUpdate.Gender = "x";
            userToUpdate.Email = anonym;
            userToUpdate.PhoneNumber = anonym;
            userToUpdate.PasswordHash = anonym;
            return userToUpdate;
        }
        public static AspNetUser ToEntityToCreate(UserModel userModel, string hashedPassword,int createdBy)
        {
            return new AspNetUser
            {
                CreatedBy = createdBy,
                CreatedOn = DateTime.Now,
                Birthday = userModel.Birthday,
                Deleted = false,
                DateCreated = DateTime.Now,
                Email = userModel.Email,
                Enabled = true,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                UserName = userModel.UserName,
                Gender = userModel.Gender,
                PhoneNumber = userModel.PhoneNr,
                PasswordHash = hashedPassword,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEndDateUtc = null,
                LockoutEnabled = true
            };
        }
        public static AspNetUser ToEntityToCreateClient(UserClientModel userModel,string hashedPassword)
        {
            return new AspNetUser
            {
                CreatedBy = 7,
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
                PasswordHash = hashedPassword,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEndDateUtc = null,
                LockoutEnabled = true
            };
        }
        public static AspNetUser ToEntityToCreate(UserRequestModel userModel, string hashedPassword,int createdBy)
        {
            return new AspNetUser
            {
                CreatedBy = createdBy,
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
                PasswordHash = hashedPassword,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEndDateUtc = null,
                LockoutEnabled = true
            };
        }
        public static List<UserModel> ToModel(IEnumerable<AspNetUser> user)
        {
            return user.Select(ToModel).ToList();
        }
    }
}
