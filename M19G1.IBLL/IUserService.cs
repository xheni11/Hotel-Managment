

using M19G1.Models;
using System.Collections.Generic;

namespace M19G1.IBLL
{
    public interface IUserService
    {
        void CreateUser(UserModel userModel,string hash,int createdBy);
        void UpdateUserActivity(int userId);
        void DeleteUser(int idUser);
        UserModel GetLoginUser(string username, string password);
        List<UserModel> GetAllUsers();        
        List<UserModel> GetUsersOrderBy(string sortField, string search,int idCurrentUser);
        UserModel GetUserById(int id);
        void ResetPassword(int idUser, string hashedPassword);
        void CreateUser(UserRequestModel userRequest,string hash,int createdBy);
        void MakeUserAnonymous(int id);
        bool UsernameExists(string username);
        bool EmailExists(string email);
        bool UsernameExists(string username,int id);
        bool EmailExists(string email,int id);
        IEnumerable<UserModel> GetNotAnonymous(int currentUser);
        void UpdateIsUserLoged(int userId);
        void UpdateUser(UserModel userModel);
        string GetEmailTemplate(string path);
    }
}
