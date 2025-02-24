using Microsoft.AspNetCore.Mvc;
using netcore__Assignment_4.Interfaces;
using netcore__Assignment_4.Services;

namespace netcore__Assignment_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("add")]
        public IActionResult AddStudent([FromBody] Student student)
        {
            _studentService.AddStudent(student);
            return Ok(new { Message = "Student added successfully", Student = student });
        }

        [HttpGet("all")]
        public ActionResult<List<Student>> GetAllStudents()
        {
            return Ok(_studentService.GetAllStudents());
        }

        
    }
}
