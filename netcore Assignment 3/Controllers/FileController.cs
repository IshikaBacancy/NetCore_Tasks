using Microsoft.AspNetCore.Mvc;
using netcore_Assignment3.Services;
using System;
using System.Threading.Tasks;

namespace netcore_Assignment3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController:ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FileController> _logger;

        public FileController(IFileService fileService, ILogger<FileController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveLog([FromBody] string logMessage)
        {
            if (string.IsNullOrWhiteSpace(logMessage))
            {
                return BadRequest("Log message cannot be empty.");
            }

            string fileName = "logs.txt";
            string logEntry = $"{DateTime.UtcNow}: {logMessage}";

            await _fileService.SaveToFileAsync(fileName, logEntry);

            return Ok("Log saved successfully.");
        }

        [HttpGet("read")]
        public async Task<IActionResult> ReadLog()
        {
            string fileName = "logs.txt";
            string content = await _fileService.ReadFromFileAsync(fileName);

            if (content == "File does not exist." || content == "Error reading file.")
            {
                return NotFound(content);
            }

            return Ok(content);
        }

        


    }
}
