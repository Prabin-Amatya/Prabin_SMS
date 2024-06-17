using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using NuGet.Common;
using Prabin_SMS.Infrastructure.IRepository;
using Prabin_SMS.Models.Entity;
using Prabin_SMS.web.Models;

namespace Prabin_SMS.web.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private readonly ICRUDServices<Course> _course;
        private readonly ICRUDServices<Degree> _degree;
        private readonly ICRUDServices<Student> _student;
        private readonly ICRUDServices<Teacher> _teacher;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ICRUDServices<StudentCourse> _studentCourse;

        public TeacherController(ICRUDServices<Course> course, ICRUDServices<Student> student, UserManager<ApplicationUser> user, ICRUDServices<StudentCourse> studentCourse, ICRUDServices<Teacher> teacher, ICRUDServices<Degree> degree)
        {
            _course = course;
            _student = student;
            _user = user;
            _studentCourse = studentCourse;
            _teacher = teacher;
            _degree = degree;
        }

        public async Task<IActionResult> Index()
        {
            var teacher = await _teacher.GetAllAsync();
            return View(teacher);
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddEdit(int id)
        {
            Teacher teacher = new Teacher();
            if (id != 0)
            {
                teacher = await _teacher.GetAsync(id);
            }
            return View(teacher);

        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(Teacher teacher)
        {
            var UserId = _user.GetUserId(HttpContext.User);
            if (teacher.Id == 0)
            {
                teacher.CreatedBy = UserId;
                teacher.CreatedDate = DateTime.Now;
                await _teacher.InsertAsync(teacher);
            }
            if (teacher.Id != 0)
            {
                Teacher updated_teacher = new Teacher();
                updated_teacher.FirstName = teacher.FirstName;
                updated_teacher.LastName = teacher.LastName;
                updated_teacher.Email = teacher.Email;
                updated_teacher.PhoneNumber = teacher.PhoneNumber;
                updated_teacher.ModifiedDate = DateTime.Now;
                updated_teacher.ModifiedBy = UserId;
                await _teacher.UpdateAsync(updated_teacher);
            }

            return RedirectToAction(nameof(Index));

        }


    }
}
