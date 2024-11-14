using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Controllers
{
    [Route("authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public AuthenticationController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLogin loginData)
        {
            if (string.IsNullOrEmpty(loginData.Username))
            {
                return BadRequest("Please input a valid username.");
            }
            if (string.IsNullOrEmpty(loginData.Password))
            {
                return BadRequest("Please input a valid password.");
            }
            var user = _userService.ValidateUser(loginData);

            if (user != null)
            {
                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
                var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);
                var claimsForToken = new List<Claim>();

                var jwtSecurityToken = new JwtSecurityToken(
                    _config["Authentication:Issuer"],
                    _config["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signature);

                var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                ResponseForLogin loginRsp = new ResponseForLogin()
                {
                    Status = 200,
                    Message = "User authenticated successfully.",
                    Token = tokenToReturn
                };

                return Ok(loginRsp);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserForCreation body)
        {
            if (string.IsNullOrEmpty(body.Username))
            {
                return BadRequest("Please input a valid username.");
            }
            if (string.IsNullOrEmpty(body.Password))
            {
                return BadRequest("Please input a valid password.");
            }
            if (string.IsNullOrEmpty(body.Email))
            {
                return BadRequest("Please input a valid email address.");
            }

            if (body != null)
            {
                UserForCreation userForCreation = new()
                {
                    Username = body.Username,
                    Password = body.Password,
                    Email = body.Email,
                };
                _userService.AddUser(userForCreation);

                ResponseForReg regRsp = new ResponseForReg()
                {
                    Message = "User created successfully."
                };

                return CreatedAtAction(nameof(Register), regRsp);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
