using Microsoft.AspNetCore.Mvc;
using netcore_Assignment3.GuidServices;
using netcore_Assignment3.Services;


namespace netcore_Assignment3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuidController : ControllerBase
    {
        private readonly SingletonGuidService _singletonService;
        private readonly ScopedGuidService _scopedService;
        private readonly TransientGuidService _transientService;

        public GuidController(SingletonGuidService singletonService,
                              ScopedGuidService scopedService,
                              TransientGuidService transientService)
        {
            _singletonService = singletonService;
            _scopedService = scopedService;
            _transientService = transientService;
        }

        [HttpGet]
        public IActionResult GetGuids()
        {
            return Ok(new
            {
                Singleton = _singletonService.GetGuid(),
                Scoped = _scopedService.GetGuid(),
                Transient = _transientService.GetGuid()
            });
        }
    }
}
