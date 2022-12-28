using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetProject.Domain;
using PetProject.Domain.Interfaces;
using PetProject.Infacstructure.Interfaces;
using System.Linq.Expressions;

namespace PetProject.Infacstructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _dbSet;
        public GenericRepository(IDataContext dataContext)
        {
            _dbSet = dataContext.GetDbContext().Set<TEntity>();
        }

        public virtual TEntity Update(TEntity entity, long userId)
        {
            entity.UpdatedTime = DateTime.UtcNow;
            entity.UpdatedBy = userId;
            return _dbSet.Update(entity).Entity;
        }
        
        public virtual void UpdateRange(ICollection<TEntity> entities, long userId)
        {
            var enumerable = entities.AsEnumerable().Select(s =>
            {
                s.UpdatedTime = DateTime.UtcNow;
                s.UpdatedBy = userId;
                return s;
            });
            _dbSet.UpdateRange(enumerable);
        }

        public virtual TEntity? Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual ValueTask<TEntity?> FindAsync(params object[] keyValues)
        {
            return _dbSet.FindAsync(keyValues);
        }

        public virtual ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return _dbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual TEntity Insert(TEntity entity, long userId)
        {
            entity.CreatedBy = userId;
            entity.CreatedTime = DateTime.UtcNow;
            return _dbSet.Add(entity).Entity;
        }

        public virtual void InsertRange(ICollection<TEntity> entities, long userId)
        {
            var enumerable = entities.Select(entity =>
            {
                entity.CreatedBy = userId;
                entity.CreatedTime = DateTime.UtcNow;
                return entity;
            }).AsEnumerable();
            _dbSet.AddRange(enumerable);
        }

        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, long userId, CancellationToken cancellationToken = default)
        {
            entity.CreatedTime = DateTime.UtcNow;
            entity.CreatedBy = userId;
            return _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual Task InsertRangeAsync(ICollection<TEntity> entities, long userId, CancellationToken cancellationToken = default)
        {
            var enumerable = entities.AsEnumerable().Select(s =>
            {
                s.CreatedTime = DateTime.UtcNow;
                s.UpdatedBy = userId;
                return s;
            });
            return _dbSet.AddRangeAsync(enumerable, cancellationToken);
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return;
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void DeleteRange(ICollection<TEntity> entities)
        {
            var enumerable = entities.AsEnumerable();
            _dbSet.RemoveRange(enumerable);
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return _dbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate)
        {
            return Queryable().Where(predicate);
        }

        public virtual IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return orderBy(Queryable(predicate));
        }

        public virtual IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties)
        {
            var result = Queryable(predicate, orderBy);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(includeProperty);
            }
            return result;
        }
    }
}
