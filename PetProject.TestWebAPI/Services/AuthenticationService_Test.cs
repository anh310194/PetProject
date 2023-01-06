using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using PetProject.TestWebAPI.Mock;
using PetProject.Utilities.Exceptions;
using PetProject.WebAPI.Services;

namespace PetProject.TestWebAPI.Services
{
    public class AuthenticationService_Test
    {
        private AuthenticationService authenticationService;
        private Mock<IHttpContextAccessor> mockAccessor;
        private Mock<IConfiguration> mockConfiguration;

        public AuthenticationService_Test()
        {
            mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(s => s["Jwt:Issuer"]).Returns("Test_Issuer");
            mockConfiguration.SetupGet(s => s["Jwt:Audience"]).Returns("Test_Audience");
            mockConfiguration.SetupGet(s => s["Jwt:Key"]).Returns("Test_Key");
            mockConfiguration.SetupGet(s => s["Jwt:ExpiredTime"]).Returns("36000");

            var userToken = MockJwt.MockUserTokenAdmin();
            mockAccessor = MockJwt.MockHttpContextAccessor(userToken);
            authenticationService = new AuthenticationService(mockAccessor.Object, mockConfiguration.Object);
        }

        [Test]
        public void GetTokenModel_NullParameter()
        {
            Assert.Throws<PetProjectException>(() => authenticationService.GetTokenModel(null));
        }
    }
}
