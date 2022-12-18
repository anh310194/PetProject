using System.Linq.Expressions;
using PetProject.Entities;
using PetProject.Shared.Common;
using PetProject.Specification.Interfaces;

namespace PetProject.Specification.Common
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
            return _unitOfWork.AsQuery<TEntity>();
        }

        public virtual IQueryable<TEntity> Queryable<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : BaseEntity
        {
            return _unitOfWork.AsQuery(match);
        }
    }
}
