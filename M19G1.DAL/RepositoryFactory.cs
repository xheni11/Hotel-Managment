using M19G1.DAL.Persistence;

namespace M19G1.DAL
{
    public static class RepositoryFactory
    {
        public static BaseRepository<TEntity> CreateRepository<TEntity>(M19G1Context dbContext)
            where TEntity : class, new() =>
            new BaseRepository<TEntity>(dbContext);
    }
}
