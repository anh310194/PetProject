﻿using PetProject.Utilities.Exceptions;
using PetProject.Utilities.Helper;
using PetProject.Models;
using PetProject.Interfaces.Repositories;
using PetProject.Utilities;
using PetProject.Interfaces.Business;

namespace PetProject.Business.Services;

public class UserService : BaseService, IUserService
{
    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<SignInModel> Authenticate(string? userName, string? password)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            throw new PetProjectApplicationException(PetProjectMessage.USER_NAME_PASSWORD_EMPTY);
        }

        var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(userName);
        if (user == null)
        {
            throw new PetProjectApplicationException(string.Format(PetProjectMessage.NOT_FOUND_USER_NAME, userName));
        }
        else if (SecurityHelper.VerifyHashedPassword(user.Password, password, user.SaltPassword))
        {
            return new SignInModel()
            {
                UserName = userName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                Roles = (await _unitOfWork.StoreProcedureRepository.GetRolesBysUserId(user.Id)).ToArray()
            };
        }
        else
        {
            throw new PetProjectApplicationException(PetProjectMessage.LoginFail);
        }
        
    }

}