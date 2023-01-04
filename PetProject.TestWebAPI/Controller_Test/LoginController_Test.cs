using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PetProject.Business.Interfaces;
using PetProject.Business.Models;
using PetProject.TestWebAPI.Mock;
using PetProject.WebAPI.Controllers;
using PetProject.WebAPI.Interfaces;
using PetProject.WebAPI.Models.Requestes;

namespace PetProject.TestWebAPI.Controller_Test
{
    public class LoginController_Test
    {
        private Mock<IUserService> _mockUserService;
        private Mock<IAuthenticationService> _mockAuthenticationService;
        private LoginController _loginController;
        private SignInRequestModel _signInRequestModel;

        public LoginController_Test()
        {
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockUserService = new Mock<IUserService>();
            _loginController = new LoginController(_mockUserService.Object, _mockAuthenticationService.Object);
            _signInRequestModel = new SignInRequestModel() { Password = "sysadmin", UserName = "sysadmin" };
        }

        [Test]
        public async Task TestParameter_Null()
        {
            //Act
            var response = await _loginController.Index(null);

            //Assert
            Assert.IsTrue(response.Result is BadRequestResult);
        }

        [Test]
        public async Task TestUserName_Empty()
        {
            //Act
            var response = await _loginController.Index(new SignInRequestModel() { UserName = "sysadmin" });

            //Assert
            Assert.IsTrue(response.Result is BadRequestObjectResult);
        }

        [Test]
        public async Task TestPassword_Empty()
        {
            //Act
            var response = await _loginController.Index(new SignInRequestModel() { Password = "sysadmin" });

            //Assert
            Assert.IsTrue(response.Result is BadRequestObjectResult);
        }

        [Test]
        public async Task Login_Failure()
        {
            //Arrange
            _mockUserService.Setup(s => s.Authenticate(_signInRequestModel.UserName, _signInRequestModel.Password));

            //Act
            var response = await _loginController.Index(_signInRequestModel);

            //Assert
            Assert.IsTrue(response.Result is BadRequestObjectResult);
        }

        [Test]
        public async Task Login_Successed()
        {
            //Arrange
            var userToken = MockJwt.MockUserTokenAdmin();
            var signInModel = new SignInModel()
            {
                FirstName = userToken.FirstName,
                LastName = userToken.LastName,
                Roles = userToken.Roles?.ToArray(),
                UserName = userToken.UserName,
                UserType = userToken.UserType,
            };
           _mockUserService.Setup(s => s.Authenticate(_signInRequestModel.UserName, _signInRequestModel.Password)).ReturnsAsync(signInModel);
            _mockAuthenticationService.Setup(s => s.GetTokenModel(signInModel)).Returns(new WebAPI.Models.Responses.TokenModel());

            //Act
            var response = await _loginController.Index(_signInRequestModel);

            //Assert
            Assert.IsTrue(response.Value != null);
        }
    }
}
