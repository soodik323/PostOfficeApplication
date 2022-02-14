using System;
using System.Collections.Generic;
using PostOffice.Application.Contracts.Persistence;
using PostOffice.Persistence.Repositories;

namespace PostOffice.Persistence.Helpers
{
    public class EfRepositoryFactory : IRepositoryFactory
    {
        private readonly Dictionary<Type, Func<IDataContext, object>> _customRepositoryFactories
            = GetCustomRepoFactories();

        private static Dictionary<Type, Func<IDataContext, object>> GetCustomRepoFactories()
        {
            return new Dictionary<Type, Func<IDataContext, object>>()
            {
                //{typeof(IApiUserRepository), (dataContext) => new ApiUserRepository(dataContext) },
                //{typeof(IContactRepository), (dataContext) => new EFContactRepository(dataContext as ApplicationDbContext) },
                //{typeof(IContactTypeRepository), (dataContext) => new EFContactTypeRepository(dataContext as ApplicationDbContext) },
            };
        }

        public Func<IDataContext, object> GetCustomRepositoryFactory<TRepoInterface>() where TRepoInterface : class
        {
            _customRepositoryFactories.TryGetValue(
                typeof(TRepoInterface),
                out Func<IDataContext, object> factory
            );
            return factory;
        }

        public Func<IDataContext, object> GetStandardRepositoryFactory<TEntity>() where TEntity : class
        {

            return (dataContext) => new EfRepository<TEntity>(dataContext);
        }
    }
}
