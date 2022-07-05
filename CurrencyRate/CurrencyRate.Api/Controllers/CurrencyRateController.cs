using CurrencyRate.Infrastructure.Services.Intefraces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyRateController : ControllerBase
    {
        private readonly ICurrencyRateService _currencyRateImplementation;

        public CurrencyRateController(
            ICurrencyRateService currencyRateImplementation
        )
        {
            _currencyRateImplementation = currencyRateImplementation;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get(DateTime? date = null, string code = null)
        {
            string result = await _currencyRateImplementation.GetJsonCurrencyRate(date, code);
            if (string.IsNullOrEmpty(result))
                return NoContent();
            else
                return Ok(result);
        }
    }
}
