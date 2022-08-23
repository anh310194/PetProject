using Microsoft.AspNetCore.Mvc;
using PetProject.Business.Interfaces;
using PetProject.Business.Model;

namespace PetProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService;
            this._logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountries()
        {
            _logger.LogInformation("Get All Countries");
            return await _countryService.GetCountries();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryModel>> GetCountryById(int id)
        {
            var result = await _countryService.GetCountryById(id);
            if (result == null)
            {
                return BadRequest();
            }
            return result;
        }

        [HttpPost()]
        public async Task<ActionResult<CountryModel>> InsertCountry(CountryModel model)
        {
            var result = await _countryService.UpsertCountryById(model);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CountryModel>> UpdateCountryById(int id, CountryModel model)
        {
            model.Id = id;
            var result = await _countryService.UpsertCountryById(model);
            return result;
        }

        /// <summary>
        /// Deletes a specific Country.
        /// </summary>
        /// <param name="id">Identity of Country</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteCountryById(int id)
        {
            _countryService.DeleteCountryById(id);
            return NoContent();
        }
    }
}
