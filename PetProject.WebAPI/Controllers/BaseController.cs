using Microsoft.AspNetCore.Mvc;
using PetProject.WebAPI.Interfaces;
using PetProject.WebAPI.Models.Responses;

namespace PetProject.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private UserTokenModel? _currentUser;
    private IAuthenticationService _authenticationService;
    public BaseController(IAuthenticationService authenticationservice)
    {
        _authenticationService = authenticationservice;
    }

    public UserTokenModel CurrentUser
    {
        get
        {
            if (_currentUser == null)
            {
                _currentUser = _authenticationService.CurrentUser;
            }
            return _currentUser;
        }
    }
}
