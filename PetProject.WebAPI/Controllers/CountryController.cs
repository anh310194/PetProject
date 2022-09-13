﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.Business.Interfaces;
using PetProject.Business.Model;
using PetProject.WebAPI.Attributes;
using PetProject.WebAPI.Enums;
using PetProject.WebAPI.Models.Responses;
using System.Security.Principal;

namespace PetProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        protected readonly UserTokenModel CurrentUser;
        private readonly ILogger _logger;
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService, ILogger<CountryController> logger, IHttpContextAccessor accessor): base(accessor)
        {
            _countryService = countryService;
            _logger = logger;
        }
        [HttpGet]
        [RoleAuthorize(FeatureEnum.ManagementCountries)]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetAll()
        {
            _logger.LogInformation("Get All Countries");
            return await _countryService.GetCountries();
        }

        [HttpGet("{id}")]
        [RoleAuthorize(FeatureEnum.UpdateCountry, FeatureEnum.InsertCountry)]
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
        [RoleAuthorize(FeatureEnum.InsertCountry)]
        public async Task<ActionResult<CountryModel?>> InsertCountry(CountryModel model)
        {
            var result = await _countryService.UpsertCountryById(model);
            return result;
        }

        [HttpPut("{id}")]
        [RoleAuthorize(FeatureEnum.UpdateCountry)]
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
        [RoleAuthorize(FeatureEnum.DeleteCountry)]
        [HttpDelete("{id}")]
        public ActionResult DeleteCountry(long id)
        {
            _countryService.DeleteCountryById(id);
            return NoContent();
        }
    }
}
