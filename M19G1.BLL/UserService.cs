


using M19G1.DAL;
using M19G1.DAL.Entities;
using M19G1.DAL.Mapping.User;
using M19G1.DAL.Repository;
using M19G1.IBLL;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace M19G1.BLL
{
    public class UserService : IUserService 
    {
        private readonly UnitOfWork _internalUnitOfWork;
        private readonly UserRepository _usersRepository;
        private readonly RoleRepository _roleRepository;

        public UserService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
            _usersRepository = _internalUnitOfWork.AspNetUsersRepository;
            _roleRepository = _internalUnitOfWork.AspNetRolesRepository;
        }

        public void CreateUser(UserModel userModel)
        {
            AspNetUser user = new AspNetUser
            {
                Id = userModel.Id,
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
                PhoneNumber = userModel.PhoneNr
            };
            _internalUnitOfWork.AspNetUsersRepository.Insert(user);
            _internalUnitOfWork.Save();
        }
        public void UpdateUser(UserModel userModel)
        {
            AspNetUser userToUpdate= _usersRepository.GetByID(userModel.Id);
            userToUpdate= UserModelMapping.ToEntity(userModel, userToUpdate);
            List<AspNetRole> roles = new List<AspNetRole>();
            roles.Add(_roleRepository.GetRoleByName(userModel.RoleName));
            userToUpdate.AspNetRoles = roles;
            _internalUnitOfWork.AspNetUsersRepository.Update(userToUpdate);
            _internalUnitOfWork.Save();
        }

        public void DeleteUser(int idUser)
        {
            AspNetUser user = _internalUnitOfWork.AspNetUsersRepository.GetByID(idUser);
            user.Deleted = true;
            _internalUnitOfWork.AspNetUsersRepository.Update(user);
            _internalUnitOfWork.Save();
        }

        public UserModel GetLoginUser(string username, string password)
        {
            return UserModelMapping.ToModel(_usersRepository.GetUserByUsernameAndPassword(username,password));
        }

        public List<UserModel> GetAllUsers()
        {
            return UserModelMapping.ToModel(_usersRepository.GetAll());
        }

        public List<UserModel> GetUsersOrderBy(string sortField, string search, int currentUserId)
        {
            return UserModelMapping.ToModel(_usersRepository.OrderBy(_usersRepository.GetAll(), sortField,search,currentUserId));
        }

        public void UpdateUserActivity(int userId)
        {
            _usersRepository.UpdateUserActivity(userId);
            _internalUnitOfWork.Save();
        }
        public UserModel GetUserById(int id)
        {
            return UserModelMapping.ToModel( _usersRepository.GetByID(id));
        }
    }

  
}
