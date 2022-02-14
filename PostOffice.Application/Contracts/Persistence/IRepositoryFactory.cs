using System;

namespace PostOffice.Application.Contracts.Persistence
{
    public interface IRepositoryFactory
    {
        Func<IDataContext, object> GetCustomRepositoryFactory<TRepoInterface>() where TRepoInterface : class;
        Func<IDataContext, object> GetStandardRepositoryFactory<TEntity>() where TEntity : class;
    }
}
