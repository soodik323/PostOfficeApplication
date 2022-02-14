namespace PostOffice.Application.Contracts.Persistence
{
    public interface IRepositoryProvider
    {
        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class;
        TRepository GetCustomRepository<TRepository>() where TRepository : class;
    }
}
