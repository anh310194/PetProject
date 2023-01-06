using PetProject.Utilities;
using PetProject.Domain.Interfaces;
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
            throw new PetProjectApplicationException(PetProjectMessage.USER_NAME_EMTPY);
        }
        var user = _unitOfWork.UserRepository.GetUserByUserName(userName);
        if (user == null)
        {
            throw new PetProjectApplicationException(string.Format(PetProjectMessage.NOT_FOUND_USER_NAME, userName));
        }
        return user.Id;
    }
}
