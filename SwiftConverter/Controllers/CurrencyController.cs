using Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Controllers
{
    [Route("currency")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("currencies")]
        public IActionResult Get()
        {
            return Ok(_currencyService.GetAllCurrencies());
        }

        [HttpGet]
        public IActionResult Get(string code)
        {
            try
            {
                return Ok(_currencyService.GetCurrency(code));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] CurrencyForCreation body)
        {
            if (string.IsNullOrEmpty(body.Name))
            {
                return BadRequest("Please input a valid name.");
            }
            if (string.IsNullOrEmpty(body.Symbol))
            {
                return BadRequest("Please input a valid symbol.");
            }
            if (string.IsNullOrEmpty(body.ISOCode))
            {
                return BadRequest("Please input a valid ISO 4217.");
            }
            if (double.IsNaN(body.ExchangeRate))
            {
                return BadRequest("Please input a valid number.");
            }

            if (body != null)
            {
                CurrencyForCreation currencyForCreation = new()
                {
                    Name = body.Name,
                    Symbol = body.Symbol,
                    ISOCode = body.ISOCode,
                    ExchangeRate = body.ExchangeRate
                };
                _currencyService.AddCurrency(currencyForCreation);
                return Created();
            }
            else
            {
                return BadRequest($"The currency could not be created.");
            }
        }

        [HttpPatch("restore/{ISOCode}")]
        public IActionResult Restore([FromRoute] string ISOCode)
        {
            try
            {
                _currencyService.RestoreCurrency(ISOCode);
                return Ok("Currency restored successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("modify/{ISOCode}")]
        public IActionResult ModifyRate([FromBody] double newRate, [FromRoute] string ISOCode)
        {
            try
            {
                _currencyService.NewRate(newRate, ISOCode);
                return Ok("Currency updated successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{ISOCode}")]
        public IActionResult Delete([FromRoute] string ISOCode)
        {
            try
            {
                _currencyService.RemoveCurrency(ISOCode);
                return Ok("Currency removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
