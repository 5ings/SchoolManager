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

        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await context.Teachers.Include(t => t.School).ToListAsync();
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
