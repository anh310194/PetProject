using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PetProject.Business.Interfaces;
using PetProject.Business.Models;
using PetProject.TestWebAPIMock;
using PetProject.Utilities.Exceptions;
using PetProject.WebAPI.Controllers;
using PetProject.WebAPI.Interfaces;

namespace PetProject.TestWebAPIController_Test;

public class CountryController_Test
{
    private CountryModel mockCountryModel;
    private Mock<ICountryService> mockCountryService;
    private CountryController countryController;

    public CountryController_Test()
    {
        var mockLog = new Mock<ILogger<CountryController>>();
        mockCountryService = new Mock<ICountryService>();
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        mockAuthenticationService.SetupGet(s => s.CurrentUser).Returns(MockJwt.MockUserTokenAdmin());
        
        countryController = new CountryController(mockCountryService.Object, mockLog.Object, mockAuthenticationService.Object);
        mockCountryModel = new CountryModel() { CountryCode = "VN", CountryName = "Viet Nam", Id = 1 };
    }

    [Test]
    public async Task GetCountries_Ok()
    {
        mockCountryService.Setup(x => x.GetCountries()).ReturnsAsync(new List<CountryModel>() { new CountryModel() });

        // Act
        var response = await countryController.GetAll();

        // Assert
        var result = response.Value;
        Assert.IsTrue(result?.Any());
    }

    [Test]
    public async Task GetCountries_Null()
    {
        mockCountryService.Setup(x => x.GetCountries());

        // Act
        var response = await countryController.GetAll();

        // Assert
        Assert.IsNull(response.Value);
    }

    [Test]
    public async Task GetCountryById_BadRequest()
    {
        var parameter = 2;
        mockCountryService.Setup(x => x.GetCountryById(parameter));

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

        // Act
        var response = await countryController.GetCountryById(parameter);

        // Assert
        Assert.IsTrue(response.Value != null);
    }

    [Test]
    public async Task InsertCountry_Ok()
    {
        mockCountryService.Setup(x => x.InsertCountryById(countryController.CurrentUser.UserName, mockCountryModel)).ReturnsAsync(mockCountryModel);

        // Act
        var response = await countryController.InsertCountry(mockCountryModel);

        // Assert
        Assert.IsTrue(response.Value != null);
    }

    [Test]
    public void InsertCountry_Exception()
    {
        mockCountryService.Setup(x => x.InsertCountryById(countryController.CurrentUser.UserName, mockCountryModel));

        // Act
        Assert.ThrowsAsync<PetProjectException>(async () => await countryController.InsertCountry(mockCountryModel));
    }

    [Test]
    public async Task UpdateCountry_Ok()
    {
        mockCountryService.Setup(x => x.UpdateCountryById(countryController.CurrentUser.UserName, mockCountryModel)).ReturnsAsync(mockCountryModel);

        // Act
        var response = await countryController.UpdateCountry(mockCountryModel);

        // Assert
        Assert.IsTrue(response.Value != null);
    }

    [Test]
    public void UpdateCountry_Exception()
    {
        mockCountryService.Setup(x => x.UpdateCountryById(countryController.CurrentUser.UserName, mockCountryModel));

        // Act
        Assert.ThrowsAsync<PetProjectException>(async () => await countryController.UpdateCountry(mockCountryModel));
    }
}