using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Prabin_SMS.Infrastructure.IRepository;
using Prabin_SMS.Models.Entity;
using Prabin_SMS.web.Models;

namespace Prabin_SMS.web.Controllers
{
    public class DisciplineController : Controller
    {
        private readonly ICRUDServices<Course> _course;
        private readonly ICRUDServices<Student> _student;
        private readonly ICRUDServices<Degree> _degree;
        private readonly ICRUDServices<Discipline> _discipline;
        private readonly UserManager<ApplicationUser> _user;

        public DisciplineController(ICRUDServices<Course> course, ICRUDServices<Student> student, UserManager<ApplicationUser> user, ICRUDServices<Degree> degree, ICRUDServices<Discipline> discipline)
        {
            _course = course;
            _student = student;
            _user = user;
            _degree = degree;
            _discipline = discipline;
        }

        public async Task<IActionResult> Index()
        {
            var discipline =await _discipline.GetAllAsync();
            return View(discipline);
        }
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddEdit(int id)
        {
            Discipline discipline = new Discipline();
            if (id != 0)
            {
               discipline =  await _discipline.GetAsync(id);
            }
            return View(discipline);

        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                var UserId = _user.GetUserId(HttpContext.User);
                if (discipline.Id == 0)
                {
                    discipline.CreatedBy = UserId;
                    discipline.CreatedDate = DateTime.Now;
                    await _discipline.InsertAsync(discipline);
                }
                else if (discipline.Id != 0)
                {
                    Discipline updated_discipline = await _discipline.GetAsync(discipline.Id);
                    updated_discipline.IsActive = discipline.IsActive;
                    updated_discipline.Name = discipline.Name;
                    updated_discipline.ModifiedDate = DateTime.Now;
                    updated_discipline.ModifiedBy = UserId;
                    await _discipline.UpdateAsync(updated_discipline);
                }

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(AddEdit));
        }


    }
}
