using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace netcore__Assignment_5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JwtAuthorizeController : Controller
    {
        private readonly IConfiguration _configuration;

        public JwtAuthorizeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost(Name = "JwtAuthorizeController")]
        public IActionResult Post([FromQuery] string id, [FromQuery]string username, [FromQuery]string password)
        {
            var UserUsername = _configuration.GetSection("Users:Username").Value;
            var UserId = _configuration.GetSection("Users:Id").Value;
            var UserPassword = _configuration.GetSection("Users:Password").Value;

            if (id == UserId && username == UserUsername && password == UserPassword)
            {
                var token = GenerateJwtToken(id, username, password);
                return Ok(new { token });
            }
            return Unauthorized();
        }
        private string GenerateJwtToken(string id, string username, string password)
        {
            var expiration = Convert.ToDouble(_configuration.GetSection("JWT:Expiration").Value);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", id),
                new Claim("UserUsername", username),
                new Claim("UserPassword", password),
                //new Claim("Role", "Admin"),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiration),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
