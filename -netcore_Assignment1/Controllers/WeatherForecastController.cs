using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace _netcore_Assignment1.Controllers
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
        

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet(Name = "GetWeatherForecast/{id}")]
        //public int Get(int id)

        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //    //return id;
        //}

        [HttpPost]
        public IActionResult CreateForecast(WeatherForecast forecast)
        {
            try
            {


                if (forecast == null)
                {
                    return BadRequest("Invalid data");
                }
                string jsonString = JsonConvert.SerializeObject(forecast, Formatting.Indented);

                string filePath = "weatherData.txt";
                System.IO.File.AppendAllText(filePath, jsonString);

                return Ok(true);
            }
            catch(Exception ex)
            {
                return BadRequest(false);
            }
                
        }

        [HttpGet("GetForecasts")]

        public IActionResult GetForecasts()
        {
            try
            {
                string filepath = "weatherData.txt";

                if (!System.IO.File.Exists("weatherData.txt"))
                {
                    return NotFound("File not found");
                }


                string jsonstring = System.IO.File.ReadAllText("weatherData.txt");
                
                return Ok(jsonstring);
            }
            catch(Exception ex)
            {
                return BadRequest(false);
            }
            
        }


    }

    
    
}
