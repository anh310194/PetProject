using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using PetProject.Domain.Common;
using PetProject.Infacstructure.Context;
using PetProject.Interfaces.Repositories;

namespace PetProject.Repositories.Common;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet;
    public GenericRepository(PetProjectContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public virtual TEntity Update(TEntity entity, long userId)
    {
        SetBaseValueUpdate(entity, userId);
        return _dbSet.Update(entity).Entity;
    }

    public virtual void UpdateRange(ICollection<TEntity> entities, long userId)
    {
        var enumerable = entities.AsEnumerable().Select(s =>
        {
            SetBaseValueUpdate(s, userId);
            return s;
        });
        _dbSet.UpdateRange(enumerable);
    }
    private void SetBaseValueUpdate(TEntity entity, long userId)
    {
        entity.UpdatedBy = userId;
        entity.UpdatedTime = DateTime.UtcNow;
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
        SetBaseValueInsert(entity, userId);
        return _dbSet.Add(entity).Entity;
    }
    private void SetBaseValueInsert(TEntity entity, long userId)
    {
        entity.CreatedBy = userId;
        entity.CreatedTime = DateTime.UtcNow;
    }

    public virtual void InsertRange(ICollection<TEntity> entities, long userId)
    {
        var enumerable = entities.Select(entity =>
        {
            SetBaseValueInsert(entity, userId);
            return entity;
        }).AsEnumerable();
        _dbSet.AddRange(enumerable);
    }

    public virtual async ValueTask<TEntity> InsertAsync(TEntity entity, long userId)
    {
        SetBaseValueInsert(entity, userId);
        var result = await _dbSet.AddAsync(entity);
        return result.Entity;
    }
    public virtual async ValueTask<TEntity> InsertAsync(TEntity entity, long userId, CancellationToken cancellationToken)
    {
        SetBaseValueInsert(entity, userId);
        var result = await _dbSet.AddAsync(entity, cancellationToken);
        return result.Entity;
    }

    public virtual Task InsertRangeAsync(ICollection<TEntity> entities, long userId)
    {
        var enumerable = entities.AsEnumerable().Select(s =>
        {
            SetBaseValueInsert(s, userId);
            return s;
        });
        return _dbSet.AddRangeAsync(enumerable);
    }

    public virtual Task InsertRangeAsync(ICollection<TEntity> entities, long userId, CancellationToken cancellationToken)
    {
        var enumerable = entities.AsEnumerable().Select(s =>
        {
            SetBaseValueInsert(s, userId);
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
