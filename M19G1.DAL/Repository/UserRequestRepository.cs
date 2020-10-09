using M19G1.DAL.Entities;
using M19G1.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Repository
{
    public class UserRequestRepository : BaseRepository<UserRequest>
    {
        public UserRequestRepository(M19G1Context context) : base(context)
        {
        }
        public IEnumerable<UserRequest> OrderBy(IEnumerable<UserRequest> entities, string propertyName, string searchValue)
        {

            var propertyInfo = entities.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (string.IsNullOrEmpty(searchValue))
            {
                return entities.OrderBy(e => propertyInfo.GetValue(e, null));
            }
            else
            {
                return entities.Where(u=> u.FirstName.Equals(searchValue)).OrderBy(e => propertyInfo.GetValue(e, null));
            }
        }
        public int CountAllRecords(int currentUser)
        {
            return CountAllRecords(u => u.Deleted == false
                && u.Id != currentUser);
        }


        public IEnumerable<UserRequest> GetAll( int pageNumber, int pageSize, string columnName, string search, bool desc)
        {

            if (search != null && !desc)
            {
                return _dbSet.Where(u => 
                 u.Deleted == false &&
                (u.Username.Contains(search) || u.LastName.Contains(search) || u.FirstName.Contains(search) || u.Email.Contains(search))).
                     OrderBy(CreateExpression<UserRequest>(columnName)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else if (search != null && desc)
            {
                return _dbSet.Where(u => 
                 u.Deleted == false &&
                (u.Username.Contains(search) || u.LastName.Contains(search) || u.FirstName.Contains(search) || u.Email.Contains(search))).
                     OrderByDescending(CreateExpression<UserRequest>(columnName)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else if (!desc)
            {
                return _dbSet.Where(u =>  u.Deleted == false).OrderBy(CreateExpression<UserRequest>(columnName)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return _dbSet.Where(u => u.Deleted == false).OrderByDescending(CreateExpression<UserRequest>(columnName)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
