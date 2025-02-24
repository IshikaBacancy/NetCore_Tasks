using netcore__Assignment_4.Services;


namespace netcore__Assignment_4.Interfaces
{
    public interface IStudentService

    {
        void AddStudent(Student student);
        List<Student> GetAllStudents();
    }
}
