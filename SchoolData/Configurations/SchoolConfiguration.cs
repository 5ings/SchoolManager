using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolData.Entities;

namespace SchoolData.Configurations
{
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasData(
                new School
                {
                    Id = 1,
                    Name = "ППМГ Казанлък",
                    Address = "Казанлък",
                    Email = "ppmg@abv.bg",
                    PhoneNumber = "0431 123 456",
                    RoomsCount = 40,
                    CreatedOn = new DateTime(2024, 6, 1),
                    IsDeleted = false
                },
                new School
                {
                    Id = 2,
                    Name = "ОУ Стара Загора",
                    Address = "Стара Загора",
                    Email = "ou@abv.bg",
                    PhoneNumber = "042 123 457",
                    RoomsCount = 30,
                    CreatedOn = new DateTime(2026, 1, 1),
                    IsDeleted = false
                },
                new School
                {
                    Id = 3,
                    Name = "СУ Пловдив",
                    Address = "Пловдив",
                    Email = "su@abv.bg",
                    PhoneNumber = "032 555 123",
                    RoomsCount = 50,
                    CreatedOn = new DateTime(2025, 3, 15),
                    IsDeleted = false
                }
            );
        }
    }
}
