using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PetProject.Business.Interfaces;
using PetProject.Business.Models;
using PetProject.Domain.Entities;
using PetProject.UnitTest.Mock;
using PetProject.Utilities.Exceptions;
using PetProject.WebAPI.Controllers;
using PetProject.WebAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetProject.UnitTest.Controller_Test;

public class CountryController_Test
{
    private ILogger<CountryController> logger;
    private Mock<IHttpContextAccessor> mockHttpContextAccessor;
    private Mock<ICountryService> mockCountryService;

    public CountryController_Test()
    {
        var mockLog = new Mock<ILogger<CountryController>>();
        logger = mockLog.Object;
        mockHttpContextAccessor = MockJwt.MockHttpContextAccessor_Admin();
        mockCountryService = new Mock<ICountryService>();
    }


    [Test]
    public async Task GetCountries_Ok()
    {
        mockCountryService.Setup(x => x.GetCountries()).ReturnsAsync(new List<CountryModel>() { new CountryModel() });
        var countryController = new CountryController(mockCountryService.Object, logger, mockHttpContextAccessor.Object);

        // Act
        var response = await countryController.GetAll();

        // Assert
        var result = response.Value;
        Assert.IsTrue(result?.Any());
    }


    [Test]
    public async Task GetCountryById_BadRequest()
    {
        var parameter = 2;
        mockCountryService.Setup(x => x.GetCountryById(parameter));
        var countryController = new CountryController(mockCountryService.Object, logger, mockHttpContextAccessor.Object);

        // Act
        var response = await countryController.GetCountryById(parameter);

        // Assert
        Assert.IsTrue(response.Result is BadRequestResult);
    }

    [Test]
    public async Task GetCountryById_Ok()
    {
        var parameter = 2;
        mockCountryService.Setup(x => x.GetCountryById(parameter)).ReturnsAsync(new CountryModel());
        var countryController = new CountryController(mockCountryService.Object, logger, mockHttpContextAccessor.Object);

        // Act
        var response = await countryController.GetCountryById(parameter);

        // Assert
        Assert.IsTrue(response.Value != null);
    }

    [Test]
    public async Task InsertCountry_Ok()
    {
        var mockCountryModel = new CountryModel() { CountryCode = "VN", CountryName = "Viet Nam", Id = 1 };

        var countryController = new CountryController(mockCountryService.Object, logger, mockHttpContextAccessor.Object);
        mockCountryService.Setup(x => x.InsertCountryById(countryController.CurrentUser.UserName, mockCountryModel)).ReturnsAsync(mockCountryModel);

        // Act
        var response = await countryController.InsertCountry(mockCountryModel);

        // Assert
        Assert.IsTrue(response.Value != null);
    }
}