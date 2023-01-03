using Microsoft.AspNetCore.Mvc;
using PetProject.Utilities.Helper;
using PetProject.WebAPI.Enums;
using PetProject.WebAPI.Models;

namespace PetProject.WebAPI.Controllers;

public class SharedController : BaseController
{
    public SharedController(IHttpContextAccessor accessor): base(accessor)
    {

    }
    [HttpGet("Menus")]
    public ActionResult<List<MenuModel>> Menus()
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

    [HttpGet("Permissions")]
    public ActionResult<List<string?>> Permissions()
    {
        return EnumHelper.GetDisplayNames<FeatureEnum>().ToList();
    }
}

