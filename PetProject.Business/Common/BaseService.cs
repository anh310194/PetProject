using PetProject.Domain.Entities;
using PetProject.Infacstructure.Interfaces;
using PetProject.Utilities.Exceptions;

namespace PetProject.Business.Common;

public abstract class BaseService
{
    protected readonly IUnitOfWork _unitOfWork;
    public BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public long GetUserId(string? userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new PetProjectException($"The value user name is empty!");
        }
        long? userId = _unitOfWork.GenericRepository<User>().Queryable(p => p.UserName == userName).Select(s => s.Id).FirstOrDefault();
        if (userId == null)
        {
            throw new PetProjectException($"The user name: {userName} could not be found!");
        }
        return userId.Value;
    }
}
