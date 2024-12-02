using Common.DTO;
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
            try
            {
                return Ok(_userService.GetUser(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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
                try
                {
                    UserForCreation userForCreation = new()
                    {
                        Username = body.Username,
                        Password = body.Password,
                        Email = body.Email,
                    };
                    _userService.AddUser(userForCreation);

                    ResponseForPost regRsp = new ResponseForPost()
                    {
                        Message = "User created successfully."
                    };

                    return CreatedAtAction(nameof(Register), regRsp);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Creation unsuccessful.");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _userService.RemoveUser(id);
                return Ok("User removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [Authorize]
        [HttpPut("{id}/upgrade-plan")]
        public IActionResult Upgrade([FromRoute] int id, [FromBody] int newPlanId, string newPlanName)
        {
            try
            {
                _userService.UpgradePlan(id, newPlanId, newPlanName);
                return Ok("Subscription upgraded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("{id}/new-conversion")]
        public IActionResult NewConversion([FromRoute] int id, [FromBody] ConversionForCreation newConversion)
        {
            try
            {
                _userService.AddConversionHistory(id, newConversion);

                ResponseForPost res = new ResponseForPost()
                {
                    Message = "Conversion saved successfully."
                };

                return CreatedAtAction(nameof(NewConversion), res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("{id}/favorites/{code}")]
        public IActionResult ToggleFavorite([FromRoute] int id, [FromRoute] string code)
        {
            try
            {
                _userService.ToggleFavoriteCurrency(id, code);
                return Ok("Favorites updated successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}/favorites")]
        public IActionResult GetFavorites([FromRoute] int id)
        {
            try
            {
                return Ok(_userService.GetFavoriteCurrencies(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}/history")]
        public IActionResult GetHistory([FromRoute] int id)
        {
            try
            {
                return Ok(_userService.GetHistoryById(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
