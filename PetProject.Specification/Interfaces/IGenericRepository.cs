using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetProject.Shared.Common;
using System.Linq.Expressions;

namespace PetProject.Specification.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> match);
        TEntity Insert(TEntity entity, long userId);
        void InsertRange(ICollection<TEntity> entities, long userId);
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, long userId, CancellationToken cancellationToken = default);
        Task InsertRangeAsync(ICollection<TEntity> entities, long userId, CancellationToken cancellationToken = default(CancellationToken));
        TEntity Update(TEntity entity, long userId);
        void UpdateRange(ICollection<TEntity> entities, long userId);
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
