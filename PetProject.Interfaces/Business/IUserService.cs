﻿using PetProject.Models;

namespace PetProject.Interfaces.Business;

public interface IUserService
{
    Task<SignInModel> Authenticate(string? userName, string? password);
}
