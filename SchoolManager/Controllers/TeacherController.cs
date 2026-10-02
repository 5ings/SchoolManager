using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SchoolManager.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SchoolData.SchoolDbContext context;
        public TeacherController(SchoolData.SchoolDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var teachers = await context.Teachers
                .Include(x => x.School)
                .ToListAsync();

            var model = new List<SchoolManager.ViewModels.Teacher.TeacherIndexViewModel>();

            foreach (var t in teachers)
            {
                model.Add(new ViewModels.Teacher.TeacherIndexViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Subject = t.Subject,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    SchoolName = t.School.Name
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var teacher = await context.Teachers
                .Include(s => s.School)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new ViewModels.Teacher.TeacherDetailsViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Subject = teacher.Subject,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                SchoolName = teacher.School.Name
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Schools = new SelectList(
                await context.Schools
                .Where(s => !s.IsDeleted)
                .ToListAsync(),
                "Id",
                "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ViewModels.Teacher.TeacherCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var teacher = new SchoolData.Entities.Teacher
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Subject = model.Subject,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    SchoolId = model.SchoolId
                };

                context.Teachers.Add(teacher);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Schools = new SelectList(
                await context.Schools
                .Where(s => !s.IsDeleted)
                .ToListAsync(),
                "Id",
                "Name");

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new ViewModels.Teacher.TeacherEditViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Subject = teacher.Subject,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                SchoolId = teacher.SchoolId
            };

            ViewBag.Schools = new SelectList(
                await context.Schools
                .Where(s => !s.IsDeleted)
                .ToListAsync(),
                "Id",
                "Name");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ViewModels.Teacher.TeacherEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var teacher = await context.Teachers.FindAsync(id);

                if (teacher == null)
                {
                    return NotFound();
                }

                teacher.FirstName = model.FirstName;
                teacher.LastName = model.LastName;
                teacher.Subject = model.Subject;
                teacher.Email = model.Email;
                teacher.PhoneNumber = model.PhoneNumber;
                teacher.SchoolId = model.SchoolId;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Schools = new SelectList(
                await context.Schools
                .Where(s => !s.IsDeleted)
                .ToListAsync(),
                "Id",
                "Name");

            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new ViewModels.Teacher.TeacherDeleteViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await context.Teachers.FindAsync(id);

            if (teacher != null)
            {
                context.Teachers.Remove(teacher);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
