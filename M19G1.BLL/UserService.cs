
using M19G1.Common.Enums;
using M19G1.Common.Exceptions.Models;
using M19G1.DAL;
using M19G1.DAL.Entities;
using M19G1.DAL.Mappings;
using M19G1.DAL.Repository;
using M19G1.Exceptions;
using M19G1.IBLL;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

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

        public NotifficationModel CreateUser(UserModel userModel, string hashedPassword,int createdBy)
        {
            NotifficationModel alertModel = new NotifficationModel();
            if (!UsernameExists(userModel.UserName) && !EmailExists(userModel.Email))
            {
                var user = UserModelMapping.ToEntityToCreate(userModel, hashedPassword,createdBy);
                var role = _internalUnitOfWork.AspNetRolesRepository.Get(x => x.Id==userModel.RoleId).SingleOrDefault();
                user.AspNetRoles.Add(role);
                _internalUnitOfWork.AspNetUsersRepository.Insert(user);
                _internalUnitOfWork.Save();
                alertModel.Message = "User added successfuly!";
                alertModel.Type = AlertConstants.ALERT_TYPE.Success.ToString("g"); ;
                return alertModel;
            }
            else
            {
                alertModel.Message = "Please check username or email because exists a user!";
                alertModel.Type = AlertConstants.ALERT_TYPE.Danger.ToString("g");
                return alertModel;
            }
        }
        public void CreateUser(UserClientModel userModel, string hashedPassword)
        {
            if (!UsernameExists(userModel.Username) && !EmailExists(userModel.Email))
            {
                AspNetUser user = UserModelMapping.ToEntityToCreateClient(userModel, hashedPassword);
                AspNetRole role = _internalUnitOfWork.AspNetRolesRepository.Get(x => x.Name.Equals(userModel.RoleName)).SingleOrDefault();
                user.AspNetRoles.Add(role);
                _internalUnitOfWork.AspNetUsersRepository.Insert(user);
                _internalUnitOfWork.Save();
            }
        }

        public string GetEmailTemplate(string path)
        {
            try
            {
                var finalPath=HttpContext.Current.Server.MapPath(path);
                return File.ReadAllText(Path.GetFullPath(finalPath));
            }
            catch(EmailTemplateNotFoundException ex)
            {
                throw ex;
            }
        }
        public void UpdateIsUserLoged(int userId)
        {

            _usersRepository.UpdateIsUserLoged(userId);
            _internalUnitOfWork.Save();
        }
        public NotifficationModel UpdateUser(UserModel userModel)
        {
            NotifficationModel alertModel = new NotifficationModel();
            if (!UsernameExists(userModel.UserName, userModel.Id) && !EmailExists(userModel.Email, userModel.Id))
            {
                AspNetUser userToUpdate = _usersRepository.GetByID(userModel.Id);
                userToUpdate = UserModelMapping.ToEntity(userModel, userToUpdate);
                userToUpdate.AspNetRoles.Clear();
                userToUpdate.AspNetRoles.Add(_roleRepository.GetByID(userModel.RoleId));
                _internalUnitOfWork.AspNetUsersRepository.Update(userToUpdate);
                _internalUnitOfWork.Save(); 
                alertModel.Message = "User edited successfuly!";
                alertModel.Type = AlertConstants.ALERT_TYPE.Success.ToString("g"); ;
                return alertModel;
            }
            else
            {
                alertModel.Message = "Please check username or email because exists a user!";
                alertModel.Type = AlertConstants.ALERT_TYPE.Danger.ToString("g");
                return alertModel;
            }       
        }

        public NotifficationModel DeleteUser(int idUser)
        {
            NotifficationModel alertModel = new NotifficationModel();
            try
            { 
            AspNetUser user = _internalUnitOfWork.AspNetUsersRepository.GetByID(idUser);
            _internalUnitOfWork.AspNetUsersRepository.SoftDelete(user);
            _internalUnitOfWork.Save();
            alertModel.Message = "User edited successfuly!";
            alertModel.Type = AlertConstants.ALERT_TYPE.Success.ToString("g");
            alertModel.Type = AlertConstants.ALERT_TYPE.Success.ToString("g");
            }
            catch(Exception ex)
            {
                alertModel.Message = "There was a problem!"+ex;
                alertModel.Type = AlertConstants.ALERT_TYPE.Danger.ToString("g");
            }
            return alertModel;
        }

        public UserModel GetLoginUser(string username, string password)
        {
            return UserModelMapping.ToModel(_usersRepository.GetUserByUsernameAndPassword(username, password));
        }

        public List<UserModel> GetAllUsers()
        {
            return UserModelMapping.ToModel(_usersRepository.GetAll());
        }

        //public List<UserModel> GetUsersOrderBy(string sortField, string search, int currentUserId)
        //{
        //    return UserModelMapping.ToModel(_usersRepository.OrderBy(_usersRepository.GetNotAnonymous(currentUserId), sortField, search, currentUserId));
        //}

        public NotifficationModel UpdateUserActivity(int userId)
        {
            NotifficationModel alertModel = new NotifficationModel();
            try
            {
                _usersRepository.UpdateUserActivity(userId);
                _internalUnitOfWork.Save();
                alertModel.Message = "User edited successfuly!";
                alertModel.Type = AlertConstants.ALERT_TYPE.Success.ToString("g");
            }
            catch(Exception ex)
            {
                alertModel.Message = "There was a problem!"+ex;
                alertModel.Type = AlertConstants.ALERT_TYPE.Danger.ToString("g");
            }
            return alertModel;
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
        public void ResetPassword(int idUser, string hashedPassword)
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
        public IEnumerable<UserModel> GetNotAnonymous(int currentUser, bool desc, string columnName, string search,int pageNumber,int pageSize)
        {

                return UserModelMapping.ToModel(_usersRepository.GetNotAnonymous(currentUser, pageNumber, pageSize,columnName,search,desc)); 

        }
        
        public int CountAllNotAnonymous(int currentUser)
        {
            return _usersRepository.CountAllRecords(currentUser);
        }
     
    }
}
