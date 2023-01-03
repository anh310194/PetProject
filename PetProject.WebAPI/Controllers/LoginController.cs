﻿using Microsoft.AspNetCore.Mvc;
using PetProject.Business.Interfaces;
using PetProject.WebAPI.Models.Requestes;
using PetProject.WebAPI.Models.Responses;

namespace PetProject.WebAPI.Controllers;

public class LoginController : BaseController
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    public LoginController(ILogger<LoginController> logger, IConfiguration configuration, IUserService userService, IHttpContextAccessor accessor) : base(accessor)
    {
        _configuration = configuration;
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<TokenModel>> Index(SignInRequestModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }
        if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
        {
            return BadRequest("the User Name or password don't not empty");
        }

        var user = await _userService.Authenticate(model.UserName, model.Password);
        if (user == null)
        {
            return BadRequest("the User Name or password invalid!");
        }

        return GetTokenModel(_configuration, user);
    }
}
