using Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAllUsers());
        }

        [Authorize]
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            return Ok(_userService.GetUser(username));
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

        [Authorize]
        [HttpDelete("{username}")]
        public IActionResult Delete([FromRoute] string username)
        {
            var deleted = _userService.RemoveUser(username);
            if (deleted) return Ok();
            return BadRequest($"User {username} could not be deleted: already removed or does not exist.");
        }
    }
}
