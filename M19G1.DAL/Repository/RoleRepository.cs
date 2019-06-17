using M19G1.DAL.Entities;
using M19G1.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Repository
{
    public class RoleRepository : BaseRepository<AspNetRole>
    {
        public RoleRepository(M19G1Context context) : base(context)
        {

        }
        public List<AspNetRole> GetRoleByName(List<string> roleName)

        {
            return _dbSet.Where(entity => entity.Name.Equals(roleName) && entity.Deleted==false).ToList();
        }

    }
}
