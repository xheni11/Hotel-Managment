

using M19G1.Models;
using System.Collections.Generic;

namespace M19G1.IBLL
{
    public interface IUserService
    {
        void CreateUser(UserModel userModel);
        void UpdateUserActivity(int userId);
        void DeleteUser(int idUser);
        UserModel GetLoginUser(string username, string password);
        List<UserModel> GetAllUsers();        
        List<UserModel> GetUsersOrderBy(string sortField, string search,int idCurrentUser);
        UserModel GetUserById(int id);
        bool IsUserValid(UserModel user);
        void GenerateNewPassword();
    }
}
