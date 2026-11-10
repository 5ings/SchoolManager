using SchoolData.Entities;

namespace SchoolManager.Core.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> GetStudentWithTeachersAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student, List<int> teacherIds);
        Task DeleteStudentAsync(int id);
    }
}
