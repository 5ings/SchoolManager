using Microsoft.EntityFrameworkCore;
using SchoolData;
using SchoolData.Entities;
using SchoolManager.Core.Interfaces;

namespace SchoolManager.Core.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly SchoolDbContext context;

        public SchoolService(SchoolDbContext context)
        {
            this.context = context;
        }

        public async Task<List<School>> GetAllSchoolsAsync(string? search = null, int? minRooms = null, string? sortOrder = null)
        {
            var query = context.Schools.Where(s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.Name.Contains(search));
            }

            if (minRooms.HasValue)
            {
                query = query.Where(s => s.RoomsCount >= minRooms.Value);
            }

            switch (sortOrder)
            {
                case "rooms_desc":
                    query = query.OrderByDescending(s => s.RoomsCount);
                    break;
                case "rooms_asc":
                    query = query.OrderBy(s => s.RoomsCount);
                    break;
                case "name":
                default:
                    query = query.OrderBy(s => s.Name);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<School?> GetSchoolByIdAsync(int id)
        {
            return await context.Schools.FindAsync(id);
        }

        public async Task AddSchoolAsync(School school)
        {
            context.Schools.Add(school);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSchoolAsync(School school)
        {
            await context.SaveChangesAsync();
        }

        // soft delete - училището остава в базата, но е маркирано като изтрито
        public async Task DeleteSchoolAsync(int id)
        {
            var school = await context.Schools.FindAsync(id);

            if (school != null)
            {
                school.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
