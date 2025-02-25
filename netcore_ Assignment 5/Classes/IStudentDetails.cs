namespace netcore__Assignment_5.Classes
{
    public interface IStudentDetails
    {
        public string AddStudent(Student student);
        public string RemoveStudent(Student student);
        public string UpdateStudent(Student student);
        public List<Student>? GetStudents();
    }
}
