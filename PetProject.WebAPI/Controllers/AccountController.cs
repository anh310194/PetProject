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
        [HttpGet("Menu")]
        public ActionResult<List<MenuModel>> Menu()
        {
            var result = new List<MenuModel>() {
                new MenuModel()
                {
                    Route= "/",
                    Name= "Dashboard",      
                    Type= "link",
                    Icon= "dashboard"
                }
            };

            var masterData = new MenuModel()
            {
                Route = "MasterData",
                Name = "MasterData",
                Type = "sub",
                Icon = "subject",
                Children = new MenuChildrenItem[]
                {
                    new MenuChildrenItem()
                    {
                        Route = "/country",
                        Name= "Country",
                        Type= "link",
                    }
                }
            };
            result.Add(masterData);
            return result;
        }
    }
}
