using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetProject.Core.Entities;
using PetProject.Core.Interfaces;
using System.Linq.Expressions;

namespace PetProject.Core.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private DbContext _context { get; }
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> match)
        {
            return _dbSet.FirstOrDefaultAsync(match);
        }

        public virtual ValueTask<TEntity?> FindAsync(params object[] keyValues)
        {
            return _dbSet.FindAsync(keyValues);
        }

        public virtual ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return _dbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.CreatedTime = DateTime.UtcNow;
            return _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual Task InsertRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var enumerable = entities.AsEnumerable().Select(s => { s.CreatedTime = DateTime.UtcNow; return s; });
            return _dbSet.AddRangeAsync(enumerable, cancellationToken);
        }

        public virtual TEntity Update(TEntity entity)
        {
            entity.UpdatedTime = DateTime.UtcNow;
            return _dbSet.Update(entity).Entity;
        }

        public virtual void DeleteRange(ICollection<TEntity> entities)
        {
            var enumerable = entities.AsEnumerable();
            _dbSet.RemoveRange(enumerable);
        }

        public virtual TEntity? Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public virtual void InsertRange(ICollection<TEntity> entities)
        {
            var enumerable = entities.AsEnumerable();
            _dbSet.AddRange(enumerable);
        }

        public virtual void UpdateRange(ICollection<TEntity> entities)
        {
            var enumerable = entities.AsEnumerable().Select(s => { s.UpdatedTime = DateTime.UtcNow; return s; });
            _dbSet.UpdateRange(enumerable);
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

        public virtual IQueryable<TEntity> Queryable()
        {
            return _dbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> query)
        {
            return Queryable().Where(query);
        }

        public virtual IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> query, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return orderBy(Queryable(query));
        }

        public virtual IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> query, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties)
        {
            var result = Queryable(query, orderBy);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(includeProperty);
            }
            return result;
        }
    }
}
