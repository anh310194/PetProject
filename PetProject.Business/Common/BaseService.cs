using PetProject.Infacstructure.Interfaces;

namespace PetProject.Business.Common;

public abstract class BaseService
{
    protected readonly IUnitOfWork _unitOfWork;
    public BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}
