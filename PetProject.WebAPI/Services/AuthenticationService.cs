using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PetProject.Business.Models;
using PetProject.Utilities;
using PetProject.Utilities.Exceptions;
using PetProject.Utilities.Extensions;
using PetProject.WebAPI.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetProject.WebAPI.Services
{
    public class AuthenticationService : Interfaces.IAuthenticationService
    {
        private UserTokenModel? _currentUser;
        private IHttpContextAccessor _accessor;
        private IConfiguration _configuration;
        public AuthenticationService(IHttpContextAccessor accessor, IConfiguration configuration)
        {
            _accessor = accessor;
            _configuration = configuration;
        }

        public UserTokenModel CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = CreateUserTokenModel();
                }
                return _currentUser;
            }
        }
        private UserTokenModel CreateUserTokenModel()
        {
            var identity = _accessor.HttpContext?.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NULL_MODEL, nameof(ClaimsIdentity)));
            }
            UserTokenModel userToken = new UserTokenModel();
            userToken.FirstName = identity.FindFirst(nameof(userToken.FirstName))?.Value.ToString();
            userToken.LastName = identity.FindFirst(nameof(userToken.LastName))?.Value.ToString();
            userToken.UserName = identity.FindFirst(nameof(userToken.UserName))?.Value.ToString();
            userToken.IdentityId = identity.FindFirst(nameof(userToken.IdentityId))?.Value.ToString();

            var userType = identity.FindFirst(nameof(userToken.UserType))?.Value.ToString();
            if (int.TryParse(userType, out int result))
            {
                userToken.UserType = result;
            }

            userToken.Roles = GetRoles(identity.FindAll(ClaimTypes.Role));

            return userToken;
        }
        private List<long>? GetRoles(IEnumerable<Claim>? permissions)
        {
            if (permissions == null)
            {
                return null;
            }
            var roles = new List<long>();
            foreach (var permission in permissions)
            {
                string value = permission.Value;
                int roleId;
                if (int.TryParse(value, out roleId))
                {
                    roles.Add(roleId);
                }
            }
            return roles;
        }

        public TokenModel GetTokenModel(SignInModel? signInUser)
        {
            if (signInUser == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NULL_MODEL, nameof(SignInModel)));
            }
            var userToken = GetUserTokenModel(signInUser);
            var TokenModel = new TokenModel()
            {
                Type = JwtBearerDefaults.AuthenticationScheme,
                ExpiredTime = _configuration.JwtExpiredTime(),
                Token = GetToken(userToken),
            };


            return TokenModel;
        }
        private UserTokenModel GetUserTokenModel(SignInModel signInUser)
        {
            if (signInUser == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NULL_MODEL, nameof(SignInModel)));
            }
            return new UserTokenModel()
            {
                FirstName = signInUser.FirstName,
                LastName = signInUser.LastName,
                IdentityId = Guid.NewGuid().ToString(),
                Roles = signInUser.Roles == null ? null : signInUser.Roles.ToList(),
                UserName = signInUser.UserName,
                UserType = signInUser.UserType

            };
        }
        private string GetToken(UserTokenModel userToken)
        {
            var claims = GetClaims(userToken);
            var jwtSecurityToken = GetJwtSecurityToken(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwtSecurityToken);
        }
        private JwtSecurityToken GetJwtSecurityToken(IEnumerable<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _configuration.JwtIssuer(),
                audience: _configuration.JwtAudience(),
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(_configuration.JwtExpiredTime()),
                signingCredentials: GetSigningCredentials());
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtKey()));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
        private List<Claim> GetClaims(UserTokenModel userToken)
        {
            if (userToken == null)
            {
                throw new PetProjectException(string.Format(PetProjectMessage.NULL_MODEL, nameof(UserTokenModel)));
            }
            var claims = new List<Claim> {
            new Claim(nameof(userToken.IdentityId), userToken.IdentityId ?? ""),
            new Claim(nameof(userToken.FirstName), userToken.FirstName ?? ""),
            new Claim(nameof(userToken.LastName), userToken.LastName ?? ""),
            new Claim(nameof(userToken.UserName), userToken.UserName ?? ""),
            new Claim(nameof(userToken.UserType), userToken.UserType.ToString())};

            if (userToken.Roles == null)
            {
                return claims;
            }

            foreach (var role in userToken.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            return claims;
        }
    }
}
