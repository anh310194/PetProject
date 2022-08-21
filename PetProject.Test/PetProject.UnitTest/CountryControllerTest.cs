using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PetProject.Business.Interfaces;
using PetProject.Business.Model;
using PetProject.WebAPI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.UnitTest
{
    public class CountryControllerTest
    {
        private CountryController countryController;
        private Mock<ICountryService> countryServerMock;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetCountries_ReturnOk()
        {
            // Arrange
            var resultCountry = new List<CountryModel>()
            {
                new CountryModel() { CountryName =  "United States", CountryCode = "US", Id = 1}
            };
            countryServerMock = new Mock<ICountryService>();
            countryServerMock.Setup(x => x.GetCountries()).ReturnsAsync(resultCountry);
            countryController = new CountryController(countryServerMock.Object);

            // Act
            var response = await countryController.GetCountries();

            // Assert
            List<CountryModel> result = response.Value.ToList<CountryModel>();
            Assert.IsTrue(result.Count > 0);
        }
    }
}