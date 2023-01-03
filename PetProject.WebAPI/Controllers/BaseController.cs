﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetProject.Utilities.Exceptions;
using PetProject.Utilities.Extensions;
using PetProject.WebAPI.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

    private UserTokenModel CreateUserTokenModel()
    {
        var identity = _accessor.HttpContext?.User.Identity as ClaimsIdentity;
        if (identity == null)
        {
            throw new PetProjectException("Could not found");
        }
        UserTokenModel result = new UserTokenModel();
        long userId;
        long.TryParse(identity.FindFirst(nameof(result.Id))?.Value, out userId);
        result.Id = userId;
        result.FirstName = identity.FindFirst(nameof(result.FirstName))?.Value.ToString();
        result.LastName = identity.FindFirst(nameof(result.LastName))?.Value.ToString();
        result.UserName = identity.FindFirst(nameof(result.UserName))?.Value.ToString();
        result.UserType = identity.FindFirst(nameof(result.UserType))?.Value.ToString();
        result.IdentityId = identity.FindFirst(nameof(result.IdentityId))?.Value.ToString();
        result.Roles = GetRoles(identity.FindAll(ClaimTypes.Role));

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

    protected TokenModel GetTokenModel(IConfiguration configuration, UserTokenModel userToken)
    {
        var TokenModel = new TokenModel()
        {
            Type = JwtBearerDefaults.AuthenticationScheme,
            ExpiredTime = configuration.JwtExpiredTime()
        };

        var jwtSecurityToken = GetJwtSecurityToken(configuration, userToken);
        TokenModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return TokenModel;
    }
    private JwtSecurityToken GetJwtSecurityToken(IConfiguration configuration, UserTokenModel userToken)
    {
        return new JwtSecurityToken(
            issuer: configuration.JwtIssuer(),
            audience: configuration.JwtAudience(),
            claims: GetClaims(userToken),
            expires: DateTime.UtcNow.AddSeconds(configuration.JwtExpiredTime()),
            signingCredentials: GetSigningCredentials(configuration));
    }

    private SigningCredentials GetSigningCredentials(IConfiguration configuration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JwtKey()));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
    private List<Claim> GetClaims(UserTokenModel userToken)
    {
        if (userToken == null)
        {
            throw new PetProjectException("The user token could not be found");
        }
        var claims = new List<Claim> {
            new Claim(nameof(userToken.IdentityId), userToken.IdentityId ?? ""),
            new Claim(nameof(userToken.Id), userToken.Id.ToString()),
            new Claim(nameof(userToken.FirstName), userToken.FirstName ?? ""),
            new Claim(nameof(userToken.LastName), userToken.LastName ?? ""),
            new Claim(nameof(userToken.UserName), userToken.UserName ?? ""),
            new Claim(nameof(userToken.UserType), userToken.UserType ?? "")};

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
