using Microsoft.Extensions.Logging;
using PetProject.Core.Interfaces;
using PetProject.Entities;
using System.Linq.Expressions;

namespace PetProject.Core
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger _logger;
        public BaseService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public virtual IQueryable<TEntity> Queryable<TEntity>() where TEntity : BaseEntity
        {
            return _unitOfWork.AsQuery<TEntity>();
        }

        public virtual IQueryable<TEntity> Queryable<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : BaseEntity
        {
            return _unitOfWork.AsQuery<TEntity>(match);
        }
    }
}
