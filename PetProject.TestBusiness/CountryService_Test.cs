using Moq;
using NUnit.Framework;
using PetProject.Business.Implements;
using PetProject.Business.Models;
using PetProject.Domain;
using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Infacstructure.Interfaces;
using PetProject.Utilities.Exceptions;

namespace PetProject.TestBusiness
{
    public class CountryService_Test
    {
        private CountryService _countryService;
        List<Country> _countries = new List<Country>()
            {
                new Country { CountryCode = "US", CountryName = "United States", Id = 1, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
                new Country { CountryCode = "VN", CountryName = "Viet Name", Id = 2, CreatedBy = 1, CreatedTime = DateTime.UtcNow },
            };
        User user = new User()
        {
            Address1 = "Test_Address1",
            Address2 = "Test_Address2",
            City = "Test_city",
            CountFailSignIn = 0,
            CountryId = 1,
            CreatedBy = 1,
            CreatedTime = DateTime.UtcNow,
            FirstName = "Test_FirstName",
            LastName = "Test_lastName",
            Password = "password",
            Status = 1,
        };

        private Mock<IUnitOfWork> _unitOfWork;
        public CountryService_Test()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _countryService = new CountryService(_unitOfWork.Object);
        }

        [Test]
        public void UpdateCountryById_UserNameEmptyOrNull()
        {
            var model = new CountryModel();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(model.Id)).ReturnsAsync(new Country());

            var assertEmtpy = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.UpdateCountryById("", model));
            Assert.IsTrue(assertEmtpy.Message == PetProjectMessage.USER_NAME_EMTPY);

            var assertNull = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.UpdateCountryById(null, model));
            Assert.IsTrue(assertNull.Message == PetProjectMessage.USER_NAME_EMTPY);
        }

        [Test]
        public void UpdateCountryById_NotFoundUserName()
        {
            var model = new CountryModel();
            string userName = "system";
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(model.Id)).ReturnsAsync(new Country());
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserName(userName));

            var exception = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.UpdateCountryById(userName, model));
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.NOT_FOUND_USER_NAME, userName));
        }

        [Test]
        public void UpdateCountryById_NotFoundCountry()
        {
            var model = new CountryModel();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(model.Id));
            var exception = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.UpdateCountryById("", model));
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.NOT_FOUND_COUNTRY_ID, model.Id));
        }
    }
}
