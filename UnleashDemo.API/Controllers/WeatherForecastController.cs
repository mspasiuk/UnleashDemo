using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unleash;

namespace UnleashDemo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUnleash _unleash;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnleash unleash)
        {
            _logger = logger;
            _unleash= unleash;
        }

        [HttpGet]
        public IActionResult Get()
        {
            
            //Use feature flag

            if (_unleash.IsEnabled("Testcontroller"))
            {
                var rng = new Random();
                var result =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
                return Ok(result);
            }
            else
            {
                var msg = "not implemented";
                return Ok(msg);
            }
            
        }
    }
}
