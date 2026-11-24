using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolData.Entities;

namespace SchoolData.Configurations
{
    public class TeacherStudentConfiguration : IEntityTypeConfiguration<TeacherStudent>
    {
        public void Configure(EntityTypeBuilder<TeacherStudent> builder)
        {
            builder.HasKey(ts => new { ts.TeacherId, ts.StudentId });

            // при изтриване се трият и записите в свързващата таблица
            builder.HasOne(ts => ts.Teacher)
                .WithMany(t => t.Students)
                .HasForeignKey(ts => ts.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ts => ts.Student)
                .WithMany(s => s.Teachers)
                .HasForeignKey(ts => ts.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new TeacherStudent
                {
                    TeacherId = 1,
                    StudentId = 1
                },
                new TeacherStudent
                {
                    TeacherId = 2,
                    StudentId = 2
                },
                new TeacherStudent
                {
                    TeacherId = 2,
                    StudentId = 3
                }
            );
        }
    }
}
