using Microsoft.AspNetCore.Mvc;
using SchoolManager.Core.Interfaces;
using SchoolManager.ViewModels.School;

namespace SchoolManager.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ISchoolService schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }

        public async Task<IActionResult> Index(string? search, int? minRooms, string? sortOrder)
        {
            var schools = await schoolService.GetAllSchoolsAsync(search, minRooms, sortOrder);

            var model = new List<SchoolIndexViewModel>();

            foreach (var s in schools)
            {
                model.Add(new SchoolIndexViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    RoomsCount = s.RoomsCount
                });
            }

            ViewBag.Search = search;
            ViewBag.MinRooms = minRooms;
            ViewBag.SortOrder = sortOrder;
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var school = await schoolService.GetSchoolByIdAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            var model = new SchoolDetailsViewModel
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
        public async Task<IActionResult> Create(SchoolCreateViewModel model)
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

                await schoolService.AddSchoolAsync(school);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var school = await schoolService.GetSchoolByIdAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            var model = new SchoolEditViewModel
            {
                Id = school.Id,
                Name = school.Name,
                Address = school.Address,
                Email = school.Email,
                PhoneNumber = school.PhoneNumber,
                RoomsCount = school.RoomsCount
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SchoolEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var school = await schoolService.GetSchoolByIdAsync(id);

                if (school == null)
                {
                    return NotFound();
                }

                school.Name = model.Name;
                school.Address = model.Address;
                school.Email = model.Email;
                school.PhoneNumber = model.PhoneNumber;
                school.RoomsCount = model.RoomsCount;

                await schoolService.UpdateSchoolAsync(school);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var school = await schoolService.GetSchoolByIdAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            var model = new SchoolDeleteViewModel
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
            await schoolService.DeleteSchoolAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
