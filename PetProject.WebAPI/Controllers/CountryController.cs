using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.Business.Interfaces;
using PetProject.Business.Model;
using PetProject.WebAPI.Attributes;
using PetProject.WebAPI.Enums;

namespace PetProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }
        [HttpGet]
        [RoleAuthorize(FeatureEnum.ManagementCountries)]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountries()
        {
            _logger.LogInformation("Get All Countries");
            return await _countryService.GetCountries();
        }

        [HttpGet("{id}")]
        [RoleAuthorize(FeatureEnum.UpdateCountry, FeatureEnum.InsertCountry)]
        public async Task<ActionResult<CountryModel>> GetCountryById(int id)
        {
            _logger.LogInformation("Get a Country by " + id);
            var result = await _countryService.GetCountryById(id);
            if (result == null)
            {
                return BadRequest();
            }
            return result;
        }

        [HttpPost()]
        [RoleAuthorize(FeatureEnum.InsertCountry)]
        public async Task<ActionResult<CountryModel?>> InsertCountry(CountryModel model)
        {
            var result = await _countryService.UpsertCountryById(model);
            return result;
        }

        [HttpPut("{id}")]
        [RoleAuthorize(FeatureEnum.UpdateCountry)]
        public async Task<ActionResult<CountryModel?>> UpdateCountryById(int id, CountryModel model)
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
        [RoleAuthorize(FeatureEnum.DeleteCountry)]
        public ActionResult DeleteCountryById(int id)
        {
            _countryService.DeleteCountryById(id);
            return NoContent();
        }
    }
}
