using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.Business.Interfaces;
using PetProject.Business.Models;
using PetProject.WebAPI.Attributes;
using PetProject.WebAPI.Enums;

namespace PetProject.WebAPI.Controllers;

[Authorize]
public class CountryController : BaseController
{
    private readonly ILogger _logger;
    private readonly ICountryService _countryService;
    public CountryController(ICountryService countryService, ILogger<CountryController> logger, IHttpContextAccessor accessor): base(accessor)
    {
        _countryService = countryService;
        _logger = logger;
    }
    [HttpGet]
    [FeatureAuthorize(FeatureEnum.ReadCountry)]
    public async Task<ActionResult<IEnumerable<CountryModel>>> GetAll()
    {
        _logger.LogInformation("Get All Countries");
        return await _countryService.GetCountries();
    }

    [HttpGet("{id}")]
    [FeatureAuthorize(FeatureEnum.UpdateCountry)]
    public async Task<ActionResult<CountryModel>> GetCountryById(long id)
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
    [FeatureAuthorize(FeatureEnum.AddCountry)]
    public async Task<ActionResult<CountryModel?>> InsertCountry(CountryModel model)
    {
        var result = await _countryService.UpsertCountryById(model);
        return result;
    }

    [HttpPut("{id}")]
    [FeatureAuthorize(FeatureEnum.UpdateCountry)]
    public async Task<ActionResult<CountryModel?>> UpdateCountry(int id, CountryModel model)
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
    [FeatureAuthorize(FeatureEnum.DeleteCountry)]
    [HttpDelete("{id}")]
    public ActionResult DeleteCountry(long id)
    {
        _countryService.DeleteCountryById(id);
        return NoContent();
    }
}
