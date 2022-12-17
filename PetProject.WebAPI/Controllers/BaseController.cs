using Microsoft.AspNetCore.Mvc;
using PetProject.WebAPI.Models.Responses;
using System.Security.Principal;

namespace PetProject.WebAPI.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserTokenModel CurrentUser;
        public BaseController(IHttpContextAccessor accessor)
        {
            CurrentUser = new UserTokenModel(accessor.HttpContext?.User.Identity);
        }
    }
}
