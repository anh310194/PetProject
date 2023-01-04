using Microsoft.EntityFrameworkCore;
using PetProject.Domain;
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
            throw new PetProjectException(PetProjectMessage.USER_NAME_EMTPY);
        }
        var user = _unitOfWork.UserRepository.GetUserByUserName(userName);
        if (user == null)
        {
            throw new PetProjectException(string.Format(PetProjectMessage.NOT_FOUND_USER_NAME, userName));
        }
        return user.Id;
    }
}
