using Moq;
using NUnit.Framework;
using PetProject.Business.Implements;
using PetProject.Business.Models;
using PetProject.Domain;
using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;
using PetProject.Utilities.Exceptions;

namespace PetProject.TestBusiness
{
    public class CountryService_Test
    {
        private CountryService _countryService;
        private Country _country;
        private User _user;
        private CountryModel countryModel;
        private Country _countryExpected;

        private Mock<IUnitOfWork> _unitOfWork;
        public CountryService_Test()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _countryService = new CountryService(_unitOfWork.Object);
            _country = new Country
            {
                CountryCode = "US",
                CountryName = "United States",
                Id = 1,
                CreatedBy = 1,
                CreatedTime = DateTime.UtcNow
            };

            _user = new User()
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
                UserName = "Test_username",
                Id = 1,
            };
            countryModel = new CountryModel()
            {
                Id = 1,
                CountryCode = "Test_CountryCode",
                CountryName = "Test_CountryName",
            };
            _countryExpected = new Country() { CountryCode = "Test_CountryCode", CountryName = "Test_CountryName", Id = 2 };

        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UpdateCountryById_UserNameEmptyOrNull(string userName)
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>())).ReturnsAsync(new Country());

            //Act
            var assertException = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.UpdateCountryById(userName, countryModel));

            //Assert
            Assert.IsTrue(assertException.Message == PetProjectMessage.USER_NAME_EMTPY);
        }

        [Test]
        public void UpdateCountryById_NotFoundUserName()
        {
            //Arrange           
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>())).ReturnsAsync(new Country());
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserName(It.IsAny<string>()));

            //Act
            var exception = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.UpdateCountryById(_user.UserName, countryModel));

            //Assert
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.NOT_FOUND_USER_NAME, _user.UserName));
        }

        [Test]
        public void UpdateCountryById_NotFoundCountry()
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>()));

            //Act
            var exception = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.UpdateCountryById("", countryModel));

            //Assert
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.NOT_FOUND_COUNTRY_ID, countryModel.Id));
        }

        [Test]
        public async Task UpdateCountryById_Successed()
        {
            //Arrange
            _unitOfWork.Reset();
            Country countryExpected = new Country() { CountryCode = countryModel.CountryCode, CountryName = countryModel.CountryName, Id = 2 };
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>())).ReturnsAsync(_country);
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserName(It.IsAny<string>())).Returns(_user);
            _unitOfWork.Setup(s => s.CountryRepository.Update(It.IsAny<Country>(), It.IsAny<long>())).Returns(countryExpected);
            _unitOfWork.Setup(s => s.SaveChangesAsync());

            //Act
            var result = await _countryService.UpdateCountryById(_user.UserName, countryModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == countryExpected.Id);
            Assert.IsTrue(result.CountryCode == countryExpected.CountryCode);
            Assert.IsTrue(result.CountryName == countryExpected.CountryName);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void InsertCountryById_UserNameEmptyOrNull(string userName)
        {
            //Arrange    
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>())).ReturnsAsync(new Country());

            //Act
            var assertException = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.InsertCountryById(userName, countryModel));

            //Assert
            Assert.IsTrue(assertException.Message == PetProjectMessage.USER_NAME_EMTPY);
        }

        [Test]
        public void InsertCountryById_NotFoundUserName()
        {
            //Arrange            
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>())).ReturnsAsync(new Country());
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserName(It.IsAny<string>()));

            //Act
            var exception = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.InsertCountryById(_user.UserName, countryModel));

            //Assert
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.NOT_FOUND_USER_NAME, _user.UserName));
        }

        [Test]
        public void InsertCountryById_CountryCodeExists()
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.GetByCountryCode(It.IsAny<string>())).Returns(_countryExpected);

            //Act
            var exception = Assert.ThrowsAsync<PetProjectException>(async () => await _countryService.InsertCountryById(_user.UserName, countryModel));

            //Assert
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.COUNTRY_CODE_EXISTS, _countryExpected.CountryCode));
        }

        [Test]
        public async Task InsertCountryById_Successed()
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserName(It.IsAny<string>())).Returns(_user);
            _unitOfWork.Setup(s => s.CountryRepository.GetByCountryCode(It.IsAny<string>()));
            _unitOfWork.Setup(s => s.CountryRepository.InsertAsync(It.IsAny<Country>(), It.IsAny<long>())).ReturnsAsync(_countryExpected);
            _unitOfWork.Setup(s => s.SaveChangesAsync());

            //Act
            var result = await _countryService.InsertCountryById(_user.UserName, countryModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == _countryExpected.Id);
            Assert.IsTrue(result.CountryCode == _countryExpected.CountryCode);
            Assert.IsTrue(result.CountryName == _countryExpected.CountryName);
        }

    }
}
