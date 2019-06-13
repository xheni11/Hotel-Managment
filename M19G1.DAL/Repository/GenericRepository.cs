using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace M19G1.DAL
{
    public abstract class GenericRepository<TEntity> where TEntity : class
    {
        internal DbContext _dbContext { get; set; }
        internal DbSet<TEntity> _dbSet;
        
        public abstract IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        public abstract TEntity GetByID(object id);

        public abstract void Insert(TEntity entity);

        public abstract void Delete(object id);

        public abstract void Delete(TEntity entityToDelete);

        public abstract void SoftDelete(TEntity entityToDelete);

        public abstract void Update(TEntity entityToUpdate);
    }
}
