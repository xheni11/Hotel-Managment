

using M19G1.Common.Exceptions.Models;
using M19G1.Models;
using System.Collections.Generic;

namespace M19G1.IBLL
{
    public interface IUserService
    {
        NotifficationModel CreateUser(UserModel userModel,string hash,int createdBy);
        NotifficationModel UpdateUserActivity(int userId);
        NotifficationModel DeleteUser(int idUser);
        UserModel GetLoginUser(string username, string password);
        List<UserModel> GetAllUsers();        
        //List<UserModel> GetUsersOrderBy(string sortField, string search,int idCurrentUser);
        UserModel GetUserById(int id);
        void ResetPassword(int idUser, string hashedPassword);
        void MakeUserAnonymous(int id);
        bool UsernameExists(string username);
        bool EmailExists(string email);
        bool UsernameExists(string username,int id);
        bool EmailExists(string email,int id);
        IEnumerable<UserModel> GetNotAnonymous(int currentUser, bool desc, string columnName, string search, int pageNumber, int pageSize);        void UpdateIsUserLoged(int userId);
        NotifficationModel UpdateUser(UserModel userModel);
        string GetEmailTemplate(string path);
        int CountAllNotAnonymous(int currentUser);
    }
}
