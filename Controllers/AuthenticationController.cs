using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OrcaProject.Models;

namespace OrcaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Invalid client request");
            }

            var user = await _userManager.FindByNameAsync(login.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddMinutes(30);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtIssuer"],
                    audience: _configuration["JwtAudience"],
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized();
        }
    }
}
