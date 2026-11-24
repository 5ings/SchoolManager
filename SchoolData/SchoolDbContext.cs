using Microsoft.EntityFrameworkCore;
using SchoolData.Configurations;
using SchoolData.Entities;

namespace SchoolData
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TeacherStudent> TeacherStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ключове, връзки и seed данни са във Fluent API конфигурациите
            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherStudentConfiguration());
        }
    }
}
