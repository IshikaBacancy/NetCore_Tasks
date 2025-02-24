using System.Collections.Generic;
using netcore__Assignment_4.Interfaces;

namespace netcore__Assignment_4.Services
{
    public class StudentService : IStudentService
    {

        private readonly List<Student> _students = new List<Student>();

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }
    }
}
