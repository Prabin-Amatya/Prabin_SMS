using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Prabin_SMS.Infrastructure.IRepository;
using Prabin_SMS.Models.Entity;
using Prabin_SMS.web.Models;

namespace Prabin_SMS.web.Controllers
{
    public class DegreeController : Controller
    {
        private readonly ICRUDServices<Course> _course;
        private readonly ICRUDServices<Student> _student;
        private readonly ICRUDServices<Teacher> _teacher;
        private readonly ICRUDServices<Degree> _degree;
        private readonly ICRUDServices<Discipline> _discipline;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ICRUDServices<StudentCourse> _studentCourse;

        public DegreeController(ICRUDServices<Course> course, ICRUDServices<Student> student, UserManager<ApplicationUser> user, ICRUDServices<StudentCourse> studentCourse, ICRUDServices<Teacher> teacher, ICRUDServices<Degree> degree, ICRUDServices<Discipline> discipline)
        {
            _course = course;
            _student = student;
            _user = user;
            _studentCourse = studentCourse;
            _teacher = teacher;
            _degree = degree;
            _discipline = discipline;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Discipline = await _discipline.GetAllAsync();
            var degree = await _degree.GetAllAsync();
            return View(degree);
        }
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Discipline = await _discipline.GetAllAsync();
            Degree degree = new Degree();
            if (id != 0)
            {
                degree = await _degree.GetAsync(id);
            }
            return View(degree);

        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Degree degree)
        {
            ViewBag.Discipline = await _discipline.GetAllAsync();
            var UserId = _user.GetUserId(HttpContext.User);
            if (degree.Id == 0)
            {
                degree.CreatedBy = UserId;
                degree.CreatedDate = DateTime.Now;
                await _degree.InsertAsync(degree);
            }
            else if (degree.Id != 0)
            {
                Degree updated_degree = await _degree.GetAsync(degree.Id);
                updated_degree.IsActive = degree.IsActive;
                updated_degree.StartDate = degree.StartDate;
                updated_degree.DegreeName = degree.DegreeName;
                updated_degree.DegreeDescription = degree.DegreeDescription;
                updated_degree.DisciplineId = degree.DisciplineId;
                updated_degree.Academic_Level = degree.Academic_Level;
                updated_degree.No_Of_Semesters = degree.No_Of_Semesters;
                updated_degree.No_Of_Years = degree.No_Of_Years;
                updated_degree.ModifiedDate = DateTime.Now;
                updated_degree.ModifiedBy = UserId;
                await _degree.UpdateAsync(updated_degree);
            }

            return RedirectToAction(nameof(Index));

        }


    }
}
