using System.Globalization;

namespace netcore__Assignment_5.Classes
{
    public class StudentDetails : IStudentDetails
    {
        private static List<Student> students = new List<Student>();
        public StudentDetails()
        {
           
        }

        public string AddStudent(Student student) {
            if (student == null)
            {
                Console.WriteLine("Student is null");
                return "Student is null\n Enter the valid student name";
            }

            var stud = students.Find(x => x.Id == student.Id);
            if (stud != null)
            {
                Console.WriteLine("Student name already exists");
                return "Student name already exists";
            }
            students.Add(student);
            Console.WriteLine("Student addded successfully");
            return "STUDENT added successfully";
        }

        public string RemoveStudent(Student student)
        {
            if (student == null)
            {
                Console.WriteLine("Student is null");
                return "Student is null";
            }

            var stud = students.Find(s => s.Id == student.Id);
            if (stud == null)
            {
                Console.WriteLine("Student does not exist");
                return "Student does not exist";
            }

            students.Remove(stud);
            Console.WriteLine("Student removed successfully");
            return "STUDENT removed successfully";
        }

        public string UpdateStudent(Student student)
        {
            if (student == null)
            {
                Console.WriteLine("Student is null");
                return "Student is null";
            }

            var stud = students.Find(s => s.Id == student.Id);
            if (stud == null)
            {
                Console.WriteLine("Student does not exist");
                return "Student does not exist";
            }

            stud.Age = student.Age;
            stud.Name = student.Name;

            Console.WriteLine("Student updated successfully");
            return "Student updated successfully";
        }

        public List<Student>? GetStudents()
        {
            if (students == null)
            {
                Console.WriteLine("No students found");
                return null;
            }
            return students;
        }
    }
}
