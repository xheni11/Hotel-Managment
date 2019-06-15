using M19G1.DAL.Entities;
using M19G1.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Repository
{
    public class LogRepository
    {
        internal DbContext _dbContext { get; set; }
        internal DbSet<Log4NetLog> _dbSet;
        public LogRepository(M19G1Context context) 
        {
            _dbContext = context;
            _dbSet = context.Set<Log4NetLog>();
        }
        public IEnumerable<Log4NetLog> GetAll()
        {
            return _dbSet.ToList();
        }
        public  Log4NetLog GetByID(object id)
        {
            return _dbSet.Find(id);
        }
    }
}
