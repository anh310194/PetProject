using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetProject.WebAPI.Models.Requestes;
using PetProject.WebAPI.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public SignController(ILogger<SignController> logger, IConfiguration configuration) 
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<TokenModel> Index(SignInModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("the User Name or password don't not empty");
            }

            var tokenModel = new TokenModel() { 
                Type = "Bearer",
                ExpiredTime = 36000
            };
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", 1.ToString()),
                        new Claim("DisplayName", "System Administrator"),
                        new Claim("UserName", "sysadmin"),
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
