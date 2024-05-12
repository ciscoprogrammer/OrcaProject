using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrcaProject.Data;
using OrcaProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrcaProject.Controllers
{
    public class AuthenticationController : Controller
    {
       
        
        public class AuthenticateController : ControllerBase
        {
            private readonly IConfiguration _configuration;
            private readonly AppDbContext _context;

            public AuthenticateController(IConfiguration configuration, AppDbContext context)
            {
                _context = context;
                _configuration = configuration;
            }

           
            [Route("api/[controller]")]
            [ApiController]
            [HttpPost]
            public IActionResult Authenticate([FromBody] User login)
            {
                if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
                {
                    return BadRequest("Invalid client request");
                }

                if (IsValidUserCredentials(login.Username, login.Password))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOptions = new JwtSecurityToken(
                        claims: new List<Claim>
                        {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Role, "Administrator") // Adjust role as needed
                        },
                        expires: DateTime.Now.AddMinutes(30), // Token expiration time
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = tokenString });
                }
                else
                {
                    return Unauthorized();
                }
            }

                private bool IsValidUserCredentials(string username, string password)
                {

                var user = _context.Users
                      .FirstOrDefault(u => u.Username == username && u.Password == password);
                return user != null;
            }
            }
        }
    }


