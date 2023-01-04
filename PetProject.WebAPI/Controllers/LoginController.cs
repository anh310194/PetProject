using Microsoft.AspNetCore.Mvc;
using PetProject.Business.Interfaces;
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
            return BadRequest("the User Name or password don't not empty");
        }

        var user = await _userService.Authenticate(model.UserName, model.Password);
        if (user == null)
        {
            return BadRequest("the User Name or password invalid!");
        }
        return _authenticationService.GetTokenModel(user);
    }
}
