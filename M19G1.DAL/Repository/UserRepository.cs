using M19G1.DAL.Entities;
using M19G1.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Repository
{
    public class UserRepository:BaseRepository<Room>
    {
        public UserRepository(M19G1Context context) : base(context)
        {

        }

       

    }
}
