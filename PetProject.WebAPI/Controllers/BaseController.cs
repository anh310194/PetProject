using Microsoft.AspNetCore.Mvc;
using PetProject.WebAPI.Models.Responses;

namespace PetProject.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly UserTokenModel CurrentUser;
    public BaseController(IHttpContextAccessor accessor)
    {
        CurrentUser = new UserTokenModel(accessor.HttpContext?.User.Identity);
    }
}
