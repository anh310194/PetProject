using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.WebAPI.Models;
using PetProject.WebAPI.Models.Responses;

namespace PetProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(IHttpContextAccessor accessor) : base(accessor)
        {
        }
        [HttpGet()]
        public ActionResult<UserTokenModel> Index()
        {
            return CurrentUser;
        }
    }
}
