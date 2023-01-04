using Microsoft.AspNetCore.Http;
using Moq;
using PetProject.Utilities.Enums;
using PetProject.WebAPI.Models.Responses;
using System.Security.Claims;

namespace PetProject.TestWebAPIMock
{
    public static class MockJwt
    {
        private static UserTokenModel? _userTokenAdmin;
        public static UserTokenModel MockUserTokenAdmin()
        {
            if (_userTokenAdmin == null)
            {
                _userTokenAdmin = new UserTokenModel()
                {
                    IdentityId = "9ef7ac43-38c7-46dd-85cd-459bc917c95a",
                    FirstName = "system",
                    LastName = "administrator",
                    UserName = "sysadmin",
                    UserType = "sysadmin",
                    Roles = Enum.GetValues<FeatureEnum>().Select(s => (long)s).ToList(),
                };
            }
            return _userTokenAdmin;
        }
        public static Mock<IHttpContextAccessor> MockHttpContextAccessor_Admin()
        {
            UserTokenModel userToken = MockUserTokenAdmin();

            var claims = GetClaimsByUserToken(userToken);
            var user = GetClaimsPrincipal(claims);
            var context = new DefaultHttpContext() { User = user };

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.SetupGet(s => s.HttpContext).Returns(context);
            return mockHttpContextAccessor;
        }
        private static ClaimsPrincipal GetClaimsPrincipal(List<Claim> claims)
        {
            ClaimsPrincipal user = new ClaimsPrincipal();
            user.AddIdentity(new ClaimsIdentity(claims));
            return user;
        }
        private static List<Claim> GetClaimsByUserToken(UserTokenModel userToken)
        {
            var claims = new List<Claim> {
            new Claim(nameof(userToken.IdentityId), userToken.IdentityId ?? ""),
            new Claim(nameof(userToken.FirstName), userToken.FirstName ?? ""),
            new Claim(nameof(userToken.LastName), userToken.LastName ?? ""),
            new Claim(nameof(userToken.UserName), userToken.UserName ?? ""),
            new Claim(nameof(userToken.UserType), userToken.UserType ?? "")};

            if (userToken.Roles != null)
            {
                foreach (var role in userToken.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
            }
            return claims;
        }
    }
}
