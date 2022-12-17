using Microsoft.IdentityModel.Tokens;
using PetProject.Business.Model;
using PetProject.Shared.Helper;
using PetProject.WebAPI.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace PetProject.WebAPI.Models.Responses
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public string? Type { get; set; }
        public double ExpiredTime { get; set; }
    }

    public class UserTokenModel
    {
        public UserTokenModel(SignInModel signInModel)
        {
            Id = signInModel.Id;
            UserName = signInModel.UserName;
            Roles = signInModel.Roles;
            FirstName = signInModel.FirstName;
            LastName = signInModel.LastName;
            UserType = signInModel.UserType;


        }
        public UserTokenModel(IIdentity? identity)
        {
            if (identity == null)
            {
                return;
            }
            var user = identity as ClaimsIdentity;

            IsAutheticated = true;
            long userId;
            if (!long.TryParse(user.FindFirst(nameof(Id))?.Value, out userId))
            {
                IsAutheticated = false;
                return;
            }
            Id = userId;
            CacheIdentity = user.FindFirst(JwtRegisteredClaimNames.Jti)?.Value.ToString();
            FirstName = user.FindFirst(nameof(FirstName))?.Value.ToString();
            LastName = user.FindFirst(nameof(LastName))?.Value.ToString();
            UserName = user.FindFirst(nameof(UserName))?.Value.ToString();
            UserType = user.FindFirst(nameof(UserType))?.Value.ToString();

            var permissions = user.FindAll(ClaimTypes.Role);

            List<long> userPermissions = new List<long>();
            foreach (var permission in permissions)
            {
                string value = permission.Value;
                int roleId;
                if (int.TryParse(value, out roleId))
                {
                    userPermissions.Add(roleId);
                }
            }
            Roles = userPermissions.ToArray();

        }
        public long UserId()
        {
            return Id;
        }
        long Id { get; set; }
        public string? UserName { get; set; }
        private long[]? Roles
        {
            get;
            set;
        }
        public string[]? Permissions
        {
            get
            {
                if (Roles == null)
                {
                    return null;
                }
                var result = new string[Roles.Length];
                for (int i = 0; i < Roles.Length; i++)
                {
                    var enumName = EnumHelper.GetName<FeatureEnum>(Roles[i]);
                    if (string.IsNullOrEmpty(enumName))
                    {
                        continue;
                    }
                    result[i] = enumName;
                }
                return result;
            }
        }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserType { get; set; }
        string CacheIdentity { get; set; }
        private string GetCacheIdentity() { return CacheIdentity; }
        bool IsAutheticated { get; set; }

        public TokenModel GetTokenModel(IConfiguration configuration)
        {
            var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"].ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(nameof(Id), Id.ToString()),
                        new Claim(nameof(FirstName), FirstName),
                        new Claim(nameof(LastName), LastName),
                        new Claim(nameof(UserName), UserName),
                        new Claim(nameof(UserType), UserType),
                    };

            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var TokenModel = new TokenModel()
            {
                Type = "Bearer",
                ExpiredTime = 36000
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddSeconds(TokenModel.ExpiredTime),
                signingCredentials: signIn);
            TokenModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return TokenModel;
        }
    }
}
