using Microsoft.AspNetCore.Mvc;

namespace _netcore_Assignment1.Controllers
{
    [ApiController]
    [Route("api/openweather")]
    public class OpenWeatherController : ControllerBase
    {
        private readonly OpenWeatherService _openWeatherService;

        public OpenWeatherController(OpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        // API to get the closest weather station info (Latitude, Longitude, Country)
        [HttpGet("GetLocation/{latitude}/{longitude}")]
        public IActionResult GetLocation(double latitude, double longitude)
        {
            try
            {
                var stationInfo = _openWeatherService.GetClosestStationInfo(latitude, longitude);
                return Ok(stationInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
