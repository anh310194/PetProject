using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetProject.Business.Interfaces;
using PetProject.WebAPI.Models.Requestes;
using PetProject.WebAPI.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public LoginController(ILogger<LoginController> logger, IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<TokenModel>> Index(SignInRequestModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("the User Name or password don't not empty");
            }

            var user = await _userService.Authenticate(model.UserName, model.Password);

            var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("DisplayName", user.FirstName +" " + user.LastName),
                        new Claim("UserName", user.UserName),
                    };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var tokenModel = new TokenModel()
            {
                Type = "Bearer",
                ExpiredTime = 36000
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddSeconds(tokenModel.ExpiredTime),
                signingCredentials: signIn);
            tokenModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return tokenModel;
        }
    }
}
