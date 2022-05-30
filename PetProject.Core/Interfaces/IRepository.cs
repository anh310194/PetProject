using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetProject.Core.Entities;
using System.Linq.Expressions;

namespace PetProject.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> match);
        TEntity Insert(TEntity entity);
        void InsertRange(ICollection<TEntity> entities);
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task InsertRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));
        TEntity Update(TEntity entity);
        void UpdateRange(ICollection<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entity);
        void DeleteRange(ICollection<TEntity> entities);
        TEntity? Find(params object[] keyValues);
        ValueTask<TEntity?> FindAsync(params object[] keyValues);
        ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken);
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> query);
        IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> query, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> query, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);
    }
}
