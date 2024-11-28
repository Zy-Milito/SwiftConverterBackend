﻿using Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.IdentityModel.Tokens.Jwt;

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
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userService.GetUser(id));
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
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _userService.RemoveUser(id);
            if (deleted) return Ok();
            return BadRequest("The user could not be deleted: already removed or does not exist.");
        }

        [HttpGet("validation")]
        public IActionResult Validate([FromHeader(Name = "Authorization")] string token)
        {
            var tokenToDecode = token;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            string subClaimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (!string.IsNullOrEmpty(subClaimValue) && int.TryParse(subClaimValue, out int sub))
            {
                return Ok(_userService.GetUser(sub));
            }
            else
            {
                return BadRequest("The 'sub' claim is not a valid integer or is missing.");
            }

        }
    }
}
