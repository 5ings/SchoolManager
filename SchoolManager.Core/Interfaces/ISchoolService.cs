using SchoolData.Entities;

namespace SchoolManager.Core.Interfaces
{
    public interface ISchoolService
    {
        Task<List<School>> GetAllSchoolsAsync(string? search = null, int? minRooms = null, string? sortOrder = null);
        Task<School?> GetSchoolByIdAsync(int id);
        Task AddSchoolAsync(School school);
        Task UpdateSchoolAsync(School school);
        Task DeleteSchoolAsync(int id);
    }
}
