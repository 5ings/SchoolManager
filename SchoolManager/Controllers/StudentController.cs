using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolData.Entities;
using SchoolManager.Core.Interfaces;
using SchoolManager.ViewModels.Student;

namespace SchoolManager.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;
        private readonly ITeacherService teacherService;

        public StudentController(IStudentService studentService, ITeacherService teacherService)
        {
            this.studentService = studentService;
            this.teacherService = teacherService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await studentService.GetAllStudentsAsync();

            var model = new List<StudentIndexViewModel>();

            foreach (var s in students)
            {
                model.Add(new StudentIndexViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    TeacherNames = string.Join(", ", s.Teachers.Select(ts => ts.Teacher.FirstName + " " + ts.Teacher.LastName))
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await studentService.GetStudentWithTeachersAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentDetailsViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                TeacherNames = string.Join(", ", student.Teachers.Select(ts => ts.Teacher.FirstName + " " + ts.Teacher.LastName))
            };

            return View(model);
        }

        private async Task LoadTeachers(List<int>? selectedTeachers = null)
        {
            var teacherList = await teacherService.GetAllTeachersAsync();

            var teachers = teacherList
                .Select(t => new
                {
                    t.Id,
                    FullName = t.FirstName + " " + t.LastName + " (" + t.Subject + ")"
                })
                .ToList();

            ViewBag.Teachers = new MultiSelectList(
                teachers,
                "Id",
                "FullName",
                selectedTeachers);
        }

        public async Task<IActionResult> Create()
        {
            await LoadTeachers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                foreach (var teacherId in model.TeacherIds.Distinct())
                {
                    // StudentId се попълва автоматично при запис
                    student.Teachers.Add(new TeacherStudent
                    {
                        TeacherId = teacherId
                    });
                }

                await studentService.AddStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }

            await LoadTeachers(model.TeacherIds);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await studentService.GetStudentWithTeachersAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentEditViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                TeacherIds = student.Teachers.Select(ts => ts.TeacherId).ToList()
            };

            await LoadTeachers(model.TeacherIds);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var student = await studentService.GetStudentWithTeachersAsync(id);

                if (student == null)
                {
                    return NotFound();
                }

                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Email = model.Email;
                student.PhoneNumber = model.PhoneNumber;

                await studentService.UpdateStudentAsync(student, model.TeacherIds);
                return RedirectToAction(nameof(Index));
            }

            await LoadTeachers(model.TeacherIds);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await studentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentDeleteViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
