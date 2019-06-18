
using M19G1.Common.RandomPassword;
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
using M19G1;
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

        public void CreateUser(UserModel userModel, string hashedPassword)
        {
            if (!UsernameExists(userModel.Username) && !EmailExists(userModel.Email))
            {
                var user = UserModelMapping.ToEntityToCreate(userModel, hashedPassword);
                var role = _internalUnitOfWork.AspNetRolesRepository.Get(x => x.Name.Equals(userModel.RoleName)).SingleOrDefault();
                user.AspNetRoles.Add(role);
                _internalUnitOfWork.AspNetUsersRepository.Insert(user);
                _internalUnitOfWork.Save();
            }
        }

        public void CreateUser(UserRequestModel userRequest, string hashedPassword)
        {
            if (!UsernameExists(userRequest.Username) && !EmailExists(userRequest.Email))
            {
                _internalUnitOfWork.AspNetUsersRepository.Insert(UserModelMapping.ToEntityToCreate(userRequest, hashedPassword));
                _internalUnitOfWork.Save();
            }
        }
        public void UpdateIsUserLoged(int userId)
        {

            _usersRepository.UpdateIsUserLoged(userId);
            _internalUnitOfWork.Save();
        }
        public void UpdateUser(UserModel userModel)
        {
            if (!UsernameExists(userModel.Username, userModel.Id) && !EmailExists(userModel.Email, userModel.Id))
            {
                AspNetUser userToUpdate = _usersRepository.GetByID(userModel.Id);
                userToUpdate = UserModelMapping.ToEntity(userModel, userToUpdate);
                userToUpdate.AspNetRoles.Clear();
                userToUpdate.AspNetRoles.Add(_roleRepository.GetRoleByName(userModel.RoleName));
                _internalUnitOfWork.AspNetUsersRepository.Update(userToUpdate);
                _internalUnitOfWork.Save();
            }
        }

        public void DeleteUser(int idUser)
        {
            AspNetUser user = _internalUnitOfWork.AspNetUsersRepository.GetByID(idUser);
            _internalUnitOfWork.AspNetUsersRepository.SoftDelete(user);
            _internalUnitOfWork.Save();
        }

        public UserModel GetLoginUser(string username, string password)
        {
            return UserModelMapping.ToModel(_usersRepository.GetUserByUsernameAndPassword(username, password));
        }

        public List<UserModel> GetAllUsers()
        {
            return UserModelMapping.ToModel(_usersRepository.GetAll());
        }

        public List<UserModel> GetUsersOrderBy(string sortField, string search, int currentUserId)
        {
            return UserModelMapping.ToModel(_usersRepository.OrderBy(_usersRepository.GetNotAnonymous(), sortField, search, currentUserId));
        }

        public void UpdateUserActivity(int userId)
        {
            _usersRepository.UpdateUserActivity(userId);
            _internalUnitOfWork.Save();
        }
        public UserModel GetUserById(int id)
        {
            return UserModelMapping.ToModel(_usersRepository.GetByID(id));
        }

        public bool UsernameExists(string username)
        {
            return _usersRepository.GetUserByUsername(username).Any();
        }
        public bool UsernameExists(string username, int idUser)
        {
            AspNetUser user = _usersRepository.GetUserByUsername(username).FirstOrDefault();
            return user != null ? user.Id != idUser : true;
        }
        public bool EmailExists(string email)
        {
            return _usersRepository.GetUserByEmail(email).Any();
        }
        public bool EmailExists(string email, int idUser)
        {
            AspNetUser user = _usersRepository.GetUserByEmail(email).FirstOrDefault();

            return user != null ? user.Id != idUser : true;

        }
        public void GenerateNewPassword(int idUser, string hashedPassword)
        {
            AspNetUser user = _internalUnitOfWork.AspNetUsersRepository.GetByID(idUser);
            user.PasswordHash = hashedPassword;
            _internalUnitOfWork.AspNetUsersRepository.Update(user);
            _internalUnitOfWork.Save();

        }
        public void MakeUserAnonymous(int id)
        {
            AspNetUser userToUpdate = _usersRepository.GetByID(id);
            userToUpdate = UserModelMapping.ToEntityToAnonymous(userToUpdate);
            _internalUnitOfWork.AspNetUsersRepository.Update(userToUpdate);
            _internalUnitOfWork.Save();

        }
        public IEnumerable<UserModel> GetNotAnonymous()
        {
            return UserModelMapping.ToModel(_usersRepository.GetNotAnonymous());
        }
    }


}
