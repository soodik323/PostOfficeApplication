using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PostOffice.Application.Contracts.Persistence
{
    public interface IRepository<TEntity>: IDisposable where TEntity : class
    {
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();
        TEntity Find(params object[] id);
        Task<TEntity> FindAsync(params object[] id);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        List<TEntity> UpdateList(List<TEntity> entityList);
        void Remove(TEntity entity);
        void Remove(params object[] id);
        Task<IEnumerable<TEntity>> SearchForAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> SearchForAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> SearchForChaining(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FromSqlAsync(string sqlQuery);
        IQueryable<TEntity> AllChaining();
        DbContext GetDbContext();
        void ExecuteRawCommand(string command);
        void ExecuteRawCommand(string command, object[] paramList);

    }
}
