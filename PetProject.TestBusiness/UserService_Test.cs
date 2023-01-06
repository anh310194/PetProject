using Moq;
using NUnit.Framework;
using PetProject.Business.Services;
using PetProject.Interfaces.Repositories;
using PetProject.Models;
using PetProject.TestBusiness.Mock;
using PetProject.Utilities;
using PetProject.Utilities.Exceptions;

namespace PetProject.TestBusiness
{
    public class UserService_Test
    {
        private UserService _userService;
        private Mock<IUnitOfWork> _unitOfWork;

        public UserService_Test()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _userService = new UserService(_unitOfWork.Object);
        }
        [Test]
        [TestCase("system", "")]
        [TestCase("system", null)]
        [TestCase("", "system")]
        [TestCase(null, "system")]
        public void Authentication_UserNamePassword_Empty(string userName, string password)
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserNameAsync(It.IsAny<string?>()));

            //Act
            var exception = Assert.ThrowsAsync<PetProjectApplicationException>(async () => await _userService.Authenticate(userName, password));

            //Assert
            Assert.IsTrue(exception.Message == PetProjectMessage.USER_NAME_PASSWORD_EMPTY);
        }

        [Test]
        [TestCase("system")]
        public void Authentication_UserName_NotFound(string userName)
        {
            //Arrange
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserNameAsync(It.IsAny<string?>()));

            //Act
            var exception = Assert.ThrowsAsync<PetProjectApplicationException>(async () => await _userService.Authenticate(userName, MockUser.PlainedPassword));

            //Assert
            Assert.IsTrue(exception.Message == string.Format(PetProjectMessage.NOT_FOUND_USER_NAME, userName));
        }

        [Test]
        public void Authentication_LoginFailure()
        {
            //Arrange
            var user = MockUser.GetUser();
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserNameAsync(It.IsAny<string?>())).ReturnsAsync(user);

            //Act
            var exception = Assert.ThrowsAsync<PetProjectApplicationException>(async () => await _userService.Authenticate(user.UserName, "passwordFailure"));

            //Assert
            Assert.IsTrue(exception.Message == PetProjectMessage.LoginFail);
        }

        private Task<SignInModel> Autneticate_Act(long[] roles)
        {
            var user = MockUser.GetUser();
            _unitOfWork.Reset();
            _unitOfWork.Setup(s => s.UserRepository.GetUserByUserNameAsync(It.IsAny<string?>())).ReturnsAsync(user);
            _unitOfWork.Setup(s => s.StoreProcedureRepository.GetRolesBysUserId(It.IsAny<long>())).ReturnsAsync(roles);

            //Act
            return _userService.Authenticate(user.UserName, MockUser.PlainedPassword);
        }

        [Test]
        [TestCase(1, 2, 4)]
        [TestCase()]
        public async Task Authentication_Roles_Exists(params long[] roles)
        {
            //Act
            var result = await Autneticate_Act(roles);

            //Assert
            Assert.IsTrue(result.Roles != null && result.Roles.Count() == roles.Length);
        }

        [Test]
        [TestCase(1, 2, 4)]
        [TestCase()]
        public async Task Authentication_Successed(params long[] roles)
        {
            var userExpected = MockUser.GetUser();
            //Act
            var result = await Autneticate_Act(roles);

            //Assert
            Assert.IsTrue(result.Roles != null && result.Roles.Count() == roles.Length);
            Assert.IsTrue(result.UserName == userExpected.UserName);
            Assert.IsTrue(result.FirstName == userExpected.FirstName);
            Assert.IsTrue(result.LastName == userExpected.LastName);
            Assert.IsTrue(result.UserType == userExpected.UserType);
        }
    }
}
