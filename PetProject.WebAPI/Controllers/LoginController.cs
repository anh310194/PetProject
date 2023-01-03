using Microsoft.AspNetCore.Mvc;
using PetProject.Business.Interfaces;
using PetProject.WebAPI.Models.Requestes;
using PetProject.WebAPI.Models.Responses;
using System;

namespace PetProject.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : BaseController
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    public LoginController(IConfiguration configuration, IUserService userService, IHttpContextAccessor accessor) : base(accessor)
    {
        _configuration = configuration;
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
        var userToken = new UserTokenModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            IdentityId = Guid.NewGuid().ToString(),
            Id = user.Id,
            Roles = user.Roles?.ToList(),
            UserName = user.UserName,
            UserType = user.UserType

        };
        return GetTokenModel(_configuration, userToken);
    }
}
