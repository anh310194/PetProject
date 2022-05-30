using Microsoft.AspNetCore.Mvc;

namespace PetProject.WebAPI.Controllers
{
    public class CountryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
