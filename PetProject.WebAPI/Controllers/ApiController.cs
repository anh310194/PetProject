using Microsoft.AspNetCore.Mvc;

namespace PetProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly ILogger _logger;
        public ApiController(ILogger logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
