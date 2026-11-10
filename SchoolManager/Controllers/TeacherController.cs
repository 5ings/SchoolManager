using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolData.Entities;
using SchoolManager.Core.Interfaces;
using SchoolManager.ViewModels.Teacher;

namespace SchoolManager.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService teacherService;
        private readonly ISchoolService schoolService;

        public TeacherController(ITeacherService teacherService, ISchoolService schoolService)
        {
            this.teacherService = teacherService;
            this.schoolService = schoolService;
        }

        private async Task LoadSchools()
        {
            ViewBag.Schools = new SelectList(
                await schoolService.GetAllSchoolsAsync(),
                "Id",
                "Name");
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await teacherService.GetAllTeachersAsync();

            var model = new List<TeacherIndexViewModel>();

            foreach (var t in teachers)
            {
                model.Add(new TeacherIndexViewModel
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
            var teacher = await teacherService.GetTeacherDetailsAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new TeacherDetailsViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Subject = teacher.Subject,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                SchoolName = teacher.School.Name,
                Students = teacher.Students
                    .Select(ts => ts.Student.FirstName + " " + ts.Student.LastName)
                    .ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            await LoadSchools();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var teacher = new Teacher
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Subject = model.Subject,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    SchoolId = model.SchoolId
                };

                await teacherService.AddTeacherAsync(teacher);
                return RedirectToAction(nameof(Index));
            }

            await LoadSchools();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await teacherService.GetTeacherByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new TeacherEditViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Subject = teacher.Subject,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                SchoolId = teacher.SchoolId
            };

            await LoadSchools();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeacherEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var teacher = await teacherService.GetTeacherByIdAsync(id);

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

                await teacherService.UpdateTeacherAsync(teacher);
                return RedirectToAction(nameof(Index));
            }

            await LoadSchools();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await teacherService.GetTeacherByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new TeacherDeleteViewModel
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
            await teacherService.DeleteTeacherAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
