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
    }
}
