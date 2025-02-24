using Microsoft.AspNetCore.Mvc;
using netcore__Assignment_4.Services;

namespace netcore__Assignment_4.Controllers
{

    [ApiController] 
    [Route("api/time")]
    public class TimeServiceController : ControllerBase
    {
        private readonly ITimeService _timeService;

        public TimeServiceController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        [HttpGet]
        public IActionResult GetCurrentTime()
        {
            return Ok(new { CurrentTime = _timeService.GetCurrentTime() });
        }


    }
}

