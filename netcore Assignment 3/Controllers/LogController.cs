using Microsoft.AspNetCore.Mvc;
using netcore_Assignment3.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace netcore_Assignment3.Controllers
{
    
   
        


   
        [ApiController]
        [Route("api/[controller]")]
        public class LogController : ControllerBase
        {
            private readonly IFileService _fileService;

            public LogController(IFileService fileService)
            {
                _fileService = fileService;
            }

            [HttpGet("read-logs")]
            public async Task<IActionResult> GetLogs()
            {
                string fileName = "logs.txt";
                List<LogEntry> logs = await _fileService.ReadLogEntriesAsync(fileName);

                if (logs.Count == 0)
                {
                    return NotFound("No logs available.");
                }

                return Ok(logs);
            }
        }
    

}

