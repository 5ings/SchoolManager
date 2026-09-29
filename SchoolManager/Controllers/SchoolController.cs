using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchoolManager.Controllers
{
    public class SchoolController : Controller
    {
        private readonly SchoolData.SchoolDbContext context;

        public SchoolController(SchoolData.SchoolDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var schools = await context.Schools
                .Where(s => !s.IsDeleted)
                .ToListAsync();

            var model = new List<SchoolManager.ViewModels.School.SchoolIndexViewModel>();

            foreach (var s in schools)
            {
                model.Add(new ViewModels.School.SchoolIndexViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    RoomsCount = s.RoomsCount
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var school = await context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            var model = new ViewModels.School.SchoolDetailsViewModel
            {
                Id = school.Id,
                Name = school.Name,
                Address = school.Address,
                Email = school.Email,
                PhoneNumber = school.PhoneNumber,
                RoomsCount = school.RoomsCount,
                CreatedOn = school.CreatedOn
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(ViewModels.School.SchoolCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var school = new SchoolData.Entities.School
                {
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    RoomsCount = model.RoomsCount,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                };

                context.Schools.Add(school);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var school = await context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            var model = new ViewModels.School.SchoolEditViewModel
            {
                Name = school.Name,
                Address = school.Address,
                Email = school.Email,
                PhoneNumber = school.PhoneNumber,
                RoomsCount = school.RoomsCount
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ViewModels.School.SchoolEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var school = await context.Schools.FindAsync(id);

                if (school == null)
                {
                    return NotFound();
                }

                school.Name = model.Name;
                school.Address = model.Address;
                school.Email = model.Email;
                school.PhoneNumber = model.PhoneNumber;
                school.RoomsCount = model.RoomsCount;

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var school = await context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }
            var model = new ViewModels.School.SchoolDeleteViewModel
            {
                Id = school.Id,
                Name = school.Name,
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var school = await context.Schools.FindAsync(id);

            if (school != null)
            {
                school.IsDeleted = true;
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
