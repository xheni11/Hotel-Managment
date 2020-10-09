using M19G1.DAL.Entities;
using M19G1.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using M19G1.Models;
using System.Reflection;

namespace M19G1.DAL.Repository
{
    public class UserRepository:BaseRepository<AspNetUser>
    {
        public UserRepository(M19G1Context context) : base(context)
        {

        }

       public AspNetUser GetUserByUsernameAndPassword(String username, String password)
        {
            return _dbSet.Where(user => user.UserName.Equals(username) && user.PasswordHash.Equals(password) && user.Deleted == false).FirstOrDefault();
        }

        public void UpdateUserActivity(int userId)
        {
            AspNetUser userToDisable = GetByID(userId);          
            userToDisable.Enabled ^= true;
            Update(userToDisable);
        }
        public void UpdateIsUserLoged(int userId)
        {
            AspNetUser userToUpdate = GetByID(userId);
            userToUpdate.IsUserLoged=true;
            Update(userToUpdate);
        }
        public IEnumerable<AspNetUser> OrderBy(IEnumerable<AspNetUser> entities, string propertyName, string searchValue,int currentUserId)
        {
           
            var propertyInfo = entities.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (string.IsNullOrEmpty(searchValue))
            {
                return entities.OrderBy(e => propertyInfo.GetValue(e, null));
            }
            else
            {
                return entities.Where(u =>u.Id!=currentUserId && u.FirstName.Contains(searchValue)).OrderBy(e => propertyInfo.GetValue(e, null));
            }
        }
        public IEnumerable<AspNetUser> GetUserByUsername(string username)
        {
            return _dbSet.Where(u => u.UserName.Equals(username));

        }
        public IEnumerable<AspNetUser> GetUserByEmail(string email)
        {
            return _dbSet.Where(u => u.Email.Equals(email));

        }
        public IEnumerable<AspNetUser> GetNotAnonymous(int currentUser,int pageNumber,int pageSize,string columnName,string search,bool desc)
        {

            if (search != null && !desc)
            {
               return _dbSet.Where(u => u.AnonymousRequest.Confirmed == false 
               || u.AnonymousRequest.Id == null 
               && u.Deleted == false 
               && u.Id != currentUser &&
               (u.UserName.Contains(search) || u.LastName.Contains(search) || u.FirstName.Contains(search) || u.Email.Contains(search))).
                    OrderBy(CreateExpression<AspNetUser>(columnName)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else if (search != null && desc)
            {
                return _dbSet.Where(u => u.AnonymousRequest.Confirmed == false
                || u.AnonymousRequest.Id == null
                && u.Deleted == false
                && u.Id != currentUser &&
                (u.UserName.Contains(search) || u.LastName.Contains(search) || u.FirstName.Contains(search) || u.Email.Contains(search))).
                     OrderByDescending(CreateExpression<AspNetUser>(columnName)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else if (!desc)
            {
                return _dbSet.Where(u => u.AnonymousRequest.Confirmed == false || u.AnonymousRequest.Id == null && u.Deleted == false && u.Id != currentUser).OrderBy(CreateExpression<AspNetUser>(columnName)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return _dbSet.Where(u => u.AnonymousRequest.Confirmed == false || u.AnonymousRequest.Id == null && u.Deleted == false && u.Id != currentUser).OrderByDescending(CreateExpression<AspNetUser>(columnName)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public  int CountAllRecords(int currentUser)
        {
           return CountAllRecords(u => u.AnonymousRequest.Confirmed == false
                || u.AnonymousRequest.Id == null
                && u.Deleted == false
                && u.Id != currentUser);
        }
      
    }
}