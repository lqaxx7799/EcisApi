using EcisApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Controllers
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
        private readonly IEmailHelper _emailHelper;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IEmailHelper emailHelper)
        {
            _logger = logger;
            _emailHelper = emailHelper;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("TestEmail")]
        public async Task<ActionResult<bool>> TestEmail([FromBody] EmailDTO payload)
        {
            await _emailHelper.SendEmailAsync(
                new string[] { payload.RecipientEmail },
                payload.Subject,
                payload.TemplateName,
                payload.Params
                );
            return Ok(true);
        }

        public class EmailDTO
        {
            public string Subject { get; set; }
            public string TemplateName { get; set; }
            public string RecipientEmail { get; set; }
            public Dictionary<string, string> Params { get; set; }
        }
    }
}
