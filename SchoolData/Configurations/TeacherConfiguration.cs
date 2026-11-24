using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolData.Entities;

namespace SchoolData.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Email)
                .IsRequired();

            builder.Property(t => t.PhoneNumber)
                .IsRequired();

            // училище с учители не може да бъде изтрито от базата
            builder.HasOne(t => t.School)
                .WithMany(s => s.Teachers)
                .HasForeignKey(t => t.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Teacher
                {
                    Id = 1,
                    FirstName = "Георги",
                    LastName = "Иванов",
                    Subject = "Математика",
                    Email = "georgiivanov@abv.bg",
                    PhoneNumber = "0888 123 456",
                    SchoolId = 1
                },
                new Teacher
                {
                    Id = 2,
                    FirstName = "Мария",
                    LastName = "Манолова",
                    Subject = "Български език",
                    Email = "mariamanolova@abv.bg",
                    PhoneNumber = "0888 123 457",
                    SchoolId = 2
                },
                new Teacher
                {
                    Id = 3,
                    FirstName = "Петър",
                    LastName = "Димитров",
                    Subject = "Физика",
                    Email = "petardimitrov@abv.bg",
                    PhoneNumber = "0888 123 459",
                    SchoolId = 3
                }
            );
        }
    }
}
