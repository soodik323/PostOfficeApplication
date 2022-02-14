using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostOffice.Application.Contracts.Persistence;

namespace PostOffice.Persistence.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext RepositoryDbContext;
        protected DbSet<TEntity> RepositoryDbSet;

        public EfRepository(IDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            RepositoryDbContext = dataContext as DbContext;

            if (RepositoryDbContext != null) RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
            if (RepositoryDbSet == null)
            {
                throw new ArgumentException("DBSet not found in dbcontext!");
            }
        }

        public DbContext GetDbContext()
        {
            return RepositoryDbContext;
        }
        public IEnumerable<TEntity> All()
        {
            return RepositoryDbSet.ToList();
        }
        public IQueryable<TEntity> AllChaining()
        {
            return RepositoryDbSet;
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await RepositoryDbSet.ToListAsync();
        }

        public TEntity Find(params object[] id)
        {
            return RepositoryDbSet.Find(id);
        }

        public async Task<TEntity> FindAsync(params object[] id)
        {
            return await RepositoryDbSet.FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            RepositoryDbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await RepositoryDbSet.AddAsync(entity);
        }

        public TEntity Update(TEntity entity)
        {
            return RepositoryDbSet.Update(entity).Entity;
        }
        public List<TEntity> UpdateList (List<TEntity> entityList)
        {
            entityList.ForEach(x =>
            {
                RepositoryDbContext.Entry(x).State = EntityState.Modified;
            });
            return entityList;
        }


        public void Remove(TEntity entity)
        {
            RepositoryDbSet.Remove(entity);
        }

        public void Remove(params object[] id)
        {
            RepositoryDbSet.Remove(Find(id));
        }

        public async Task<IEnumerable<TEntity>> SearchForAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await RepositoryDbSet.Where(predicate).ToListAsync();
        }
        public IQueryable<TEntity> SearchForChaining(Expression<Func<TEntity, bool>> predicate)
        {
            return RepositoryDbSet.Where(predicate);
        }
        public IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return RepositoryDbSet.Where(predicate);
        }
        public async Task<IEnumerable<TEntity>> FromSqlAsync(string sqlQuery)
        {
            return await RepositoryDbSet.FromSqlRaw(sqlQuery).ToListAsync();
        }

        public IEnumerable<TEntity> SearchForAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return RepositoryDbSet.Where(predicate).AsNoTracking();
        }
        public void ExecuteRawCommand(string command)
        {
            RepositoryDbContext.Database.ExecuteSqlRaw(command);
        }

        public void ExecuteRawCommand(string command, object[] paramList)
        {
            RepositoryDbContext.Database.ExecuteSqlRaw(command, paramList);

        }

        public async Task SaveChangesAsync()
        {
            await RepositoryDbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (RepositoryDbContext != null)
            {
                RepositoryDbContext.Dispose();
                RepositoryDbContext = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
