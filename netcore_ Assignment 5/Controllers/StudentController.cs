using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcore__Assignment_5.Classes;
namespace netcore__Assignment_5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentDetails studentDetails;
        public StudentController(IStudentDetails studentDetails)
        {
            this.studentDetails = studentDetails ?? throw new ArgumentNullException(nameof(studentDetails));
        }

        [HttpGet("GetStudents")]
        public IActionResult GetStudentsController()
        {
            var studentList = studentDetails.GetStudents();
            if (!studentList.Any())
            {
                return NotFound("No students found");
            }
            return Ok(studentList);
        }

        [HttpPost("AddStudent")]
        public IActionResult AddStudentsController([FromBody] Student student)
        {
            var msg = studentDetails.AddStudent(student);
            if (msg.Contains("added successfully", StringComparison.OrdinalIgnoreCase))
            {
                return Ok(msg);
            }
            return BadRequest(msg);
        }

        [HttpDelete("RemoveStudent")]
        public IActionResult RemoveStudentsController([FromBody] Student student)
        {
            var msg = studentDetails.RemoveStudent(student);
            if (msg == "Student removed")
            {
                return Ok(msg);
            }
            return BadRequest(msg);
        }

        [HttpPut("UpdateStudent")]
        public IActionResult UpdateStudentsController([FromBody] Student student)
        {
            var msg = studentDetails.UpdateStudent(student);
            if (msg == "Student updated")
            {
                return Ok(msg);
            }
            return BadRequest(msg);
        }


    }
}
