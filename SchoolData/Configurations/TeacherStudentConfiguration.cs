using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolData.Entities;

namespace SchoolData.Configurations
{
    public class TeacherStudentConfiguration : IEntityTypeConfiguration<TeacherStudent>
    {
        public void Configure(EntityTypeBuilder<TeacherStudent> builder)
        {
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
