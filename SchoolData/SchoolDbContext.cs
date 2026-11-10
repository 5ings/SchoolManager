using Microsoft.EntityFrameworkCore;
using SchoolData.Configurations;
using SchoolData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SchoolData
{
    public class SchoolDbContext:DbContext
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

            modelBuilder.Entity<TeacherStudent>()
                .HasKey(ts => new { ts.TeacherId, ts.StudentId });

            modelBuilder.Entity<TeacherStudent>()
                .HasOne(ts => ts.Teacher)
                .WithMany(t => t.Students)
                .HasForeignKey(ts => ts.TeacherId)
                .OnDelete(DeleteBehavior.Cascade); // при изтриване се трият и записите в свързващата таблица

            modelBuilder.Entity<TeacherStudent>()
                .HasOne(ts => ts.Student)
                .WithMany(s => s.Teachers)
                .HasForeignKey(ts => ts.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherStudentConfiguration());
        }
    }
}
