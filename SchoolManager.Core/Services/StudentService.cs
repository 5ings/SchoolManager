using Microsoft.EntityFrameworkCore;
using SchoolData;
using SchoolData.Entities;
using SchoolManager.Core.Interfaces;

namespace SchoolManager.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext context;

        public StudentService(SchoolDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await context.Students
                .Include(s => s.Teachers)
                .ThenInclude(ts => ts.Teacher)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await context.Students.FindAsync(id);
        }

        // Details, Edit GET, Edit POST
        public async Task<Student?> GetStudentWithTeachersAsync(int id)
        {
            return await context.Students
                .Include(s => s.Teachers)
                .ThenInclude(ts => ts.Teacher)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        // Create POST - учителите вече са добавени в student.Teachers от контролера
        public async Task AddStudentAsync(Student student)
        {
            context.Students.Add(student);
            await context.SaveChangesAsync();
        }

        // student трябва да е зареден със student.Teachers (GetStudentWithTeachersAsync)
        public async Task UpdateStudentAsync(Student student, List<int> teacherIds)
        {
            var selectedIds = teacherIds.Distinct().ToList();

            // махаме връзките, които вече не са избрани
            var toRemove = student.Teachers
                .Where(ts => !selectedIds.Contains(ts.TeacherId))
                .ToList();
            context.TeacherStudents.RemoveRange(toRemove);

            // добавяме само новите връзки
            var existingIds = student.Teachers.Select(ts => ts.TeacherId).ToList();
            foreach (var teacherId in selectedIds.Except(existingIds))
            {
                student.Teachers.Add(new TeacherStudent
                {
                    TeacherId = teacherId,
                    StudentId = student.Id
                });
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await context.Students
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student != null)
            {
                context.TeacherStudents.RemoveRange(student.Teachers);
                context.Students.Remove(student);
                await context.SaveChangesAsync();
            }
        }
    }
}
