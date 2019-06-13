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

    }
}
