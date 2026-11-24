using Microsoft.EntityFrameworkCore;
using SchoolData;
using SchoolData.Entities;
using SchoolManager.Core.Interfaces;

namespace SchoolManager.Core.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly SchoolDbContext context;

        public TeacherService(SchoolDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Teacher>> GetAllTeachersAsync(string? search = null, string? subject = null, string? sortOrder = null)
        {
            var query = context.Teachers.Include(t => t.School).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.FirstName.Contains(search) || t.LastName.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(subject))
            {
                query = query.Where(t => t.Subject.Contains(subject));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(t => t.FirstName)
                        .ThenByDescending(t => t.LastName);
                    break;
                case "subject":
                    query = query.OrderBy(t => t.Subject)
                        .ThenBy(t => t.FirstName);
                    break;
                case "name_asc":
                default:
                    query = query.OrderBy(t => t.FirstName)
                        .ThenBy(t => t.LastName);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            return await context.Teachers.FindAsync(id);
        }

        public async Task<Teacher?> GetTeacherDetailsAsync(int id)
        {
            return await context.Teachers
                .Include(t => t.School)
                .Include(t => t.Students)
                .ThenInclude(ts => ts.Student)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTeacherAsync(Teacher teacher)
        {
            context.Teachers.Add(teacher);
            await context.SaveChangesAsync();
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var teacher = await context.Teachers
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher != null)
            {
                context.TeacherStudents.RemoveRange(teacher.Students);
                context.Teachers.Remove(teacher);
                await context.SaveChangesAsync();
            }
        }
    }
}
