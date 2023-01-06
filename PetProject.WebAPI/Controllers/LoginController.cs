using Microsoft.AspNetCore.Mvc;
using PetProject.Interfaces.Business;
using PetProject.Utilities;
using PetProject.WebAPI.Interfaces;
using PetProject.WebAPI.Models.Requestes;
using PetProject.WebAPI.Models.Responses;

namespace PetProject.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authenticationService;
    public LoginController(IUserService userService, IAuthenticationService authenticationservice)
    {
        _userService = userService;
        _authenticationService = authenticationservice;
    }

    [HttpPost]
    public async Task<ActionResult<TokenModel>> Index(SignInRequestModel? model)
    {
        if (model == null)
        {
            return BadRequest();
        }
        if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
        {
            return BadRequest(PetProjectMessage.USER_NAME_PASSWORD_EMPTY);
        }

        var user = await _userService.Authenticate(model.UserName, model.Password);
        if (user == null)
        {
            return BadRequest(PetProjectMessage.LoginFail);
        }
        return _authenticationService.GetTokenModel(user);
    }
}
