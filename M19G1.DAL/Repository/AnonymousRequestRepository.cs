using M19G1.DAL.Entities;
using M19G1.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Repository
{
    public class AnonymousRequestRepository:BaseRepository<AnonymousRequest>
    {
        public AnonymousRequestRepository(M19G1Context context) : base(context)
        {

        }
        public void CreateAnonymousRequest(int id)
        {

        }
    }
}
