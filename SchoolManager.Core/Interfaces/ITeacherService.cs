using SchoolData.Entities;

namespace SchoolManager.Core.Interfaces
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeachersAsync(string? search = null, string? subject = null, string? sortOrder = null);
        Task<Teacher?> GetTeacherDetailsAsync(int id);
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task AddTeacherAsync(Teacher teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
    }
}
