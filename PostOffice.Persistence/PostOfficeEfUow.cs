using System;
using System.Threading.Tasks;
using PostOffice.Application.Contracts.Persistence;
using PostOffice.Domain.Entities;

namespace PostOffice.Persistence
{
    public class PostOfficeEfUow : IPostOfficeEfUow, IDisposable
    {
        private readonly PostOfficeDbContext _dbContext;
        private readonly IRepositoryProvider _repositoryProvider;

        public PostOfficeEfUow(IDataContext dataContext, IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
            _dbContext = dataContext as PostOfficeDbContext;
            if (_dbContext == null)
            {
                throw new NullReferenceException("No EF dbcontext found in UOW");
            }
        }

        
        //Standard Repository
        public IRepository<Shipment> Shipments => GetEntityRepository<Shipment>();
        public IRepository<Bag> Bags => GetEntityRepository<Bag>();
        public IRepository<Parcel> Parcels => GetEntityRepository<Parcel>();
        


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            return _repositoryProvider.GetEntityRepository<TEntity>();
        }

        public TRepositoryInterface GetCustomRepository<TRepositoryInterface>() where TRepositoryInterface : class
        {
            return _repositoryProvider.GetCustomRepository<TRepositoryInterface>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
