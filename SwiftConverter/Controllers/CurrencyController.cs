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
            return Ok(_currencyService.GetCurrency(code));
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

        [HttpPut("modify/{ISOCode}")]
        public IActionResult ModifyRate([FromBody] double newRate, [FromRoute] string ISOCode)
        {
            var updated = _currencyService.NewRate(newRate, ISOCode);
            if (updated) return Ok();
            return BadRequest("The currency specified could not be removed because it does not exist.");
        }

        [HttpDelete("{ISOCode}")]
        public IActionResult Delete([FromRoute] string ISOCode)
        {
            var deleted = _currencyService.RemoveCurrency(ISOCode);
            if (deleted) return Ok();
            return BadRequest($"The currency specified could not be deleted: already removed or does not exist.");
        }
    }
}
