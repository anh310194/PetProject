using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PetProject.Interfaces.Business;
using PetProject.Models;
using PetProject.TestWebAPI.Mock;
using PetProject.Utilities.Exceptions;
using PetProject.WebAPI.Controllers;
using PetProject.WebAPI.Interfaces;

namespace PetProject.TestWebAPI.Controller_Test;

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
        //Arrange
        mockCountryService.Reset();
        mockCountryService.Setup(x => x.GetCountries()).ReturnsAsync(new List<CountryModel>() { new CountryModel() });

        // Act
        var response = await countryController.GetAll();

        // Assert
        Assert.IsTrue(response.Any());
    }

    [Test]
    public async Task GetCountries_Null()
    {
        //Arrange
        mockCountryService.Reset();
        mockCountryService.Setup(x => x.GetCountries());

        // Act
        var response = await countryController.GetAll();

        // Assert
        Assert.IsNull(response);
    }

    [Test]
    public async Task GetCountryById_BadRequest()
    {
        //Arrange
        mockCountryService.Reset();
        mockCountryService.Setup(x => x.GetCountryById(It.IsAny<long>()));

        // Act
        var response = await countryController.GetCountryById(mockCountryModel.Id);

        // Assert
        Assert.IsTrue(response.Result is BadRequestResult);
    }

    [Test]
    public async Task GetCountryById_Ok()
    {
        //Arrange
        mockCountryService.Reset();
        mockCountryService.Setup(x => x.GetCountryById(It.IsAny<long>())).ReturnsAsync(mockCountryModel);

        // Act
        var response = await countryController.GetCountryById(mockCountryModel.Id);

        // Assert
        Assert.IsTrue(response.Value != null);
    }

    [Test]
    public async Task InsertCountry_Ok()
    {
        //Arrange
        mockCountryService.Reset();
        mockCountryService.Setup(x => x.InsertCountryById(It.IsAny<string>(), It.IsAny<CountryModel>())).ReturnsAsync(mockCountryModel);

        // Act
        var response = await countryController.InsertCountry(mockCountryModel);

        // Assert
        Assert.IsTrue(response.Value != null);
    }

    [Test]
    public void InsertCountry_Exception()
    {
        mockCountryService.Reset();
        mockCountryService.Setup(x => x.InsertCountryById(It.IsAny<string>(), It.IsAny<CountryModel>()));

        // Act
        Assert.ThrowsAsync<PetProjectApplicationException>(async () => await countryController.InsertCountry(mockCountryModel));
    }

    [Test]
    public async Task UpdateCountry_Ok()
    {
        //Arrange
        mockCountryService.Reset();
        mockCountryService.Setup(x => x.UpdateCountryById(It.IsAny<string>(), It.IsAny<CountryModel>())).ReturnsAsync(mockCountryModel);

        // Act
        var response = await countryController.UpdateCountry(mockCountryModel);

        // Assert
        Assert.IsTrue(response.Value != null);
    }

    [Test]
    public void UpdateCountry_Exception()
    {
        //Arrange
        mockCountryService.Reset();
        mockCountryService.Setup(x => x.UpdateCountryById(It.IsAny<string>(), It.IsAny<CountryModel>()));

        // Act
        Assert.ThrowsAsync<PetProjectApplicationException>(async () => await countryController.UpdateCountry(mockCountryModel));
    }
}