using System;
using System.Collections.Generic;
using PostOffice.Application.Contracts.Persistence;

namespace PostOffice.Persistence.Helpers
{
    public class EfRepositoryProvider : IRepositoryProvider
    {
        private readonly IDataContext _dbContext;
        private readonly IRepositoryFactory _repositoryFactory;

        // here we keep our already created repos!
        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();

        public EfRepositoryProvider(IDataContext dataContext, IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _dbContext = dataContext;
            if (_dbContext == null)
            {
                throw new NullReferenceException("No EF dbcontext found in UOW");
            }
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            return GetOrMakeRepository<IRepository<TEntity>>
                (_repositoryFactory.GetStandardRepositoryFactory<TEntity>());

        }

        private TRepository GetOrMakeRepository<TRepository>(Func<IDataContext, object> factory)
            where TRepository : class
        {
            if (_repositoryCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository)repo;
            }

            if (factory == null)
            {
                throw new ArgumentNullException($"No factory found for type {typeof(TRepository).Name}");
            }

            repo = factory(_dbContext);

            _repositoryCache.Add(typeof(TRepository), repo);

            return (TRepository)repo;

        }

        public TRepository GetCustomRepository<TRepository>()
            where TRepository : class
        {
            return GetOrMakeRepository<TRepository>(
                _repositoryFactory.GetCustomRepositoryFactory<TRepository>());
        }
    }
}
