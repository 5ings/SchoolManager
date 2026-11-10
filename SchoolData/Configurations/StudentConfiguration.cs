using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolData.Entities;

namespace SchoolData.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Иван",
                    LastName = "Петров",
                    Email = "ivanpetrov1@abv.bg",
                    PhoneNumber = "0888 123 456"
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Петър",
                    LastName = "Иванов",
                    Email = "petarivanov1@abv.bg",
                    PhoneNumber = "0888 123 457"
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Йордан",
                    LastName = "Йорданов",
                    Email = "iordaniordanov1@abv.bg",
                    PhoneNumber = "0888 123 458"
                }
            );
        }
    }
}
