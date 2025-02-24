using Microsoft.AspNetCore.Mvc;
using netcore_Assignment3.Services;
using System.Threading.Tasks;

namespace netcore_Assignment3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] double latitude, [FromQuery] double longitude)
        {
            // Validate that only Ahmedabad's coordinates are allowed
            if (latitude != 23.0225 || longitude != 72.5714)
            {
                return Unauthorized("Access Denied: This API only provides weather data for Ahmedabad.");
            }

            var weatherData = await _weatherService.GetWeatherAsync(latitude, longitude);
            return Ok(weatherData);
        }
    }
}

