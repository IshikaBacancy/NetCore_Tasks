using Microsoft.AspNetCore.Mvc;

namespace netcore__Assignment_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TestController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult GetHello([FromHeader(Name = "username")]string username)
        {
            return Ok(new { Message = "Hello, welcome to our API!" });
        }

    }
}
