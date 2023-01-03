using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetProject.Business.Models;
using PetProject.Utilities.Exceptions;
using PetProject.Utilities.Extensions;
using PetProject.WebAPI.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace PetProject.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private UserTokenModel _currentUser;
    private IHttpContextAccessor _accessor;
    public BaseController(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        _currentUser = CreateUserTokenModel();
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

    public UserTokenModel CreateUserTokenModel()
    {
        var user = _accessor.HttpContext?.User.Identity as ClaimsIdentity;
        if (user == null)
        {
            throw new PetProjectException("Could not found");
        }
        UserTokenModel result = new UserTokenModel();
        long userId;
        long.TryParse(user.FindFirst(nameof(result.Id))?.Value, out userId);
        result.Id = userId;
        result.FirstName = user.FindFirst(nameof(result.FirstName))?.Value.ToString();
        result.LastName = user.FindFirst(nameof(result.LastName))?.Value.ToString();
        result.UserName = user.FindFirst(nameof(result.UserName))?.Value.ToString();
        result.UserType = user.FindFirst(nameof(result.UserType))?.Value.ToString();
        result.Roles = GetRoles(user.FindAll(ClaimTypes.Role));

        return result;
    }
    private List<long> GetRoles(IEnumerable<Claim>? permissions)
    {
        var roles = new List<long>();
        if (permissions == null)
        {
            return roles;
        }
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

    public TokenModel GetTokenModel(IConfiguration configuration, SignInModel userToken)
    {
        if (userToken == null)
        {
            throw new PetProjectException("The user token could not be found");
        }
        var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(nameof(userToken.Id), userToken.Id.ToString()),
                    new Claim(nameof(userToken.FirstName), userToken.FirstName ?? ""),
                    new Claim(nameof(userToken.LastName), userToken.LastName ?? ""),
                    new Claim(nameof(userToken.UserName), userToken.UserName ?? ""),
                    new Claim(nameof(userToken.UserType), userToken.UserType ?? ""),
                };

        if (userToken.Roles != null)
        {

            foreach (var role in userToken.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
        }

        var TokenModel = new TokenModel()
        {
            Type = JwtBearerDefaults.AuthenticationScheme,
            ExpiredTime = 36000
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JwtKey()));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            configuration.JwtIssuer(),
            configuration.JwtAudience(),
            claims,
            expires: DateTime.UtcNow.AddSeconds(TokenModel.ExpiredTime),
            signingCredentials: signIn);
        TokenModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return TokenModel;
    }
}
