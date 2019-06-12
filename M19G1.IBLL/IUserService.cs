

using M19G1.Models;
using System.Collections.Generic;

namespace M19G1.IBLL
{
    public interface IUserService
    {
        void CreateDummyUser();
        void CreateDummyUserWithError();

        List<UserModel> GetAllUsers();

        UserModel GetLoginUser(string username, string password);

        void UpdateUserActivity(int userId);
        List<UserModel> GetUsersOrderBy(string sortField, string search,int idCurrentUser);
        UserModel GetUserById(int id);
    }
}
