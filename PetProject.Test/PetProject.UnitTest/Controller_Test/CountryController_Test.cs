using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PetProject.Business.Interfaces;
using PetProject.Business.Model;
using PetProject.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetProject.UnitTest.Controller_Test
{
    public class CountryController_Test
    {
        private CountryController? countryController;

        private CountryModel resultCountry = new CountryModel() { CountryName = "United States", CountryCode = "US", Id = 1 };
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetCountries_ReturnOk()
        {
            // Arrange
            var mockLog = new Mock<ILogger<CountryController>>();
            ILogger<CountryController> logger = mockLog.Object;

            Mock<ICountryService> countryServerMock = new Mock<ICountryService>();
            countryServerMock.Setup(x => x.GetCountries()).ReturnsAsync(new List<CountryModel>() { resultCountry });
            countryController = new CountryController(countryServerMock.Object, logger);

            // Act
            var response = await countryController.GetCountries();

            // Assert
            var result = response.Value;
            Assert.IsTrue(result?.Any());
        }


        [Test]
        public async Task GetCountryById_ReturnBadRequest()
        {
            // Arrange
            var mockLog = new Mock<ILogger<CountryController>>();
            ILogger<CountryController> logger = mockLog.Object;

            CountryModel countryModel = new CountryModel();
            Mock<ICountryService> countryServerMock = new Mock<ICountryService>();
            countryServerMock.Setup(x => x.GetCountryById(2)).ReturnsAsync(countryModel);
            countryController = new CountryController(countryServerMock.Object, logger);

            // Act
            var response = await countryController.GetCountryById(2);

            // Assert
            Assert.IsTrue(response.Result is BadRequestResult);
        }

        [Test]
        public async Task GetCountryById_ReturnOk()
        {
            // Arrange
            var mockLog = new Mock<ILogger<CountryController>>();
            ILogger<CountryController> logger = mockLog.Object;

            Mock<ICountryService> countryServerMock = new Mock<ICountryService>();
            countryServerMock.Setup(x => x.GetCountryById(1)).ReturnsAsync(resultCountry);
            countryController = new CountryController(countryServerMock.Object, logger);

            // Act
            var response = await countryController.GetCountryById(1);

            // Assert
            var result = response.Value;
            Assert.IsTrue(result != null);
        }
    }
}