using Microsoft.AspNetCore.Mvc;
using Share_Music.Common.CustomAttribute;
using Share_Music.Models;

namespace Share_Music.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[] {"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetUserDetails")]
        [Authorize(Role.Admin)]
        public ActionResult GetUserDeails()
        {
            return Ok(userId+userName);
        }
    }
}