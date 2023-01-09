using Moq;
using NUnit.Framework;
using PetProject.Business.Services;
using PetProject.Utilities;
using PetProject.Domain.Entities;
using PetProject.Interfaces.Repositories;
using PetProject.Utilities.Exceptions;
using PetProject.TestBusiness.Mock;
using PetProject.TestBusiness.MockData;
using PetProject.Models;

namespace PetProject.TestBusiness
{
    public class CountryService_Test
    {
        private CountryService _countryService;
        private Country _country;
        private User _user;
        private CountryModel _countryModel;
        private Country _countryExpected;

        private Mock<IUnitOfWork> _unitOfWork;
        public CountryService_Test()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _countryService = new CountryService(_unitOfWork.Object);
            _country = MockCountry.GetCountry();
            _user = MockUser.GetUser();
            _countryModel = MockCountry.GetCountryModel();
            _countryExpected = MockCountry.GetCountryExpected();

        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UpdateCountryById_UserNameEmptyOrNull_ThrowException(string userName)
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>())).ReturnsAsync(new Country());

            //Act
            var assertException = Assert.ThrowsAsync<PetProjectApplicationException>(async () => await _countryService.UpdateCountryById(userName, _countryModel));

            //Assert
            Assert.IsTrue(assertException.Message == PetProjectMessage.USER_NAME_EMTPY);
        }

        [Test]
        public void UpdateCountryById_NotFoundUserName_ThrowException()
        {
            //Arrange           
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>())).ReturnsAsync(new Country());
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserName(It.IsAny<string>()));

            //Act
            var exception = Assert.ThrowsAsync<PetProjectApplicationException>(async () => await _countryService.UpdateCountryById(_user.UserName, _countryModel));

            //Assert
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.NOT_FOUND_USER_NAME, _user.UserName));
        }

        [Test]
        public void UpdateCountryById_NotFoundCountry_ThrowException()
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>()));

            //Act
            var exception = Assert.ThrowsAsync<PetProjectApplicationException>(async () => await _countryService.UpdateCountryById("", _countryModel));

            //Assert
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.NOT_FOUND_COUNTRY_ID, _countryModel.Id));
        }

        [Test]
        public async Task UpdateCountryById_Successed()
        {
            //Arrange
            _unitOfWork.Reset();
            Country countryExpected = new Country() { CountryCode = _countryModel.CountryCode, CountryName = _countryModel.CountryName, Id = 2 };
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>())).ReturnsAsync(_country);
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserName(It.IsAny<string>())).Returns(_user);
            _unitOfWork.Setup(s => s.CountryRepository.Update(It.IsAny<Country>(), It.IsAny<long>())).Returns(countryExpected);
            _unitOfWork.Setup(s => s.SaveChangesAsync());

            //Act
            var result = await _countryService.UpdateCountryById(_user.UserName, _countryModel);

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
            var assertException = Assert.ThrowsAsync<PetProjectApplicationException>(async () => await _countryService.InsertCountryById(userName, _countryModel));

            //Assert
            Assert.IsTrue(assertException.Message == PetProjectMessage.USER_NAME_EMTPY);
        }

        [Test]
        public void InsertCountryById_NotFoundUserName()
        {
            //Arrange            
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.GetByCountryCodeAsync(It.IsAny<string?>()));
            _unitOfWork.Setup(s => s.CountryRepository.FindAsync(It.IsAny<long>())).ReturnsAsync(new Country());
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserName(It.IsAny<string>()));

            //Act
            var exception = Assert.ThrowsAsync<PetProjectApplicationException>(async () => await _countryService.InsertCountryById(_user.UserName, _countryModel));

            //Assert
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.NOT_FOUND_USER_NAME, _user.UserName));
        }

        [Test]
        public void InsertCountryById_CountryCodeExists()
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.CountryRepository.GetByCountryCodeAsync(It.IsAny<string?>())).ReturnsAsync(_countryExpected);

            //Act
            var exception = Assert.ThrowsAsync<PetProjectApplicationException>(async () => await _countryService.InsertCountryById(_user.UserName, _countryModel));

            //Assert
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.COUNTRY_CODE_EXISTS, _countryExpected.CountryCode));
        }

        [Test]
        public async Task InsertCountryById_Successed()
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserName(It.IsAny<string>())).Returns(_user);
            _unitOfWork.Setup(s => s.CountryRepository.GetByCountryCodeAsync(It.IsAny<string?>()));
            _unitOfWork.Setup(s => s.CountryRepository.InsertAsync(It.IsAny<Country>(), It.IsAny<long>())).ReturnsAsync(_countryExpected);
            _unitOfWork.Setup(s => s.SaveChangesAsync());

            //Act
            var result = await _countryService.InsertCountryById(_user.UserName, _countryModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == _countryExpected.Id);
            Assert.IsTrue(result.CountryCode == _countryExpected.CountryCode);
            Assert.IsTrue(result.CountryName == _countryExpected.CountryName);
        }

    }
}
