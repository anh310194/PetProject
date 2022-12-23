using System.Linq.Expressions;
using PetProject.Domain;
using PetProject.Domain.Interfaces;

namespace PetProject.Business.Common
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual IQueryable<TEntity> Queryable<TEntity>() where TEntity : BaseEntity
        {
            return _unitOfWork.GetRepository<TEntity>().Queryable();
        }

        public virtual IQueryable<TEntity> Queryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return _unitOfWork.GetRepository<TEntity>().Queryable(predicate);
        }
        
        public virtual TEntity Insert<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity
        {
            return _unitOfWork.GetRepository<TEntity>().Insert(entity, userId);
        }

        public virtual void InsertRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity
        {
            _unitOfWork.GetRepository<TEntity>().InsertRange(entities, userId);
        }

        public virtual TEntity Update<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity
        {
            return _unitOfWork.GetRepository<TEntity>().Update(entity, userId);
        }

        public virtual void UpdateRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity
        {
            foreach (var entity in entities)
            {
                entity.UpdatedTime = DateTime.UtcNow;
                entity.UpdatedBy = userId;
            }
            _unitOfWork.GetRepository<TEntity>().UpdateRange(entities, userId);
        }

        public virtual void Delete<TEntity>(object id) where TEntity : BaseEntity
        {
            _unitOfWork.GetRepository<TEntity>().Delete(id);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _unitOfWork.GetRepository<TEntity>().Delete(entity);
        }

        public virtual void DeleteRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity
        {
            _unitOfWork.GetRepository<TEntity>().DeleteRange(entities);
        }

        public virtual TEntity? Find<TEntity>(params object[] keyValues) where TEntity : BaseEntity
        {
            return _unitOfWork.GetRepository<TEntity>().Find(keyValues);
        }

        public virtual ValueTask<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : BaseEntity
        {
            return _unitOfWork.GetRepository<TEntity>().FindAsync(keyValues);
        }

        public virtual ValueTask<TEntity?> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            return _unitOfWork.GetRepository<TEntity>().FindAsync(keyValues, cancellationToken);
        }

    }
}
