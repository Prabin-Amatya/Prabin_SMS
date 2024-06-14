using Microsoft.AspNetCore.Mvc;
using Prabin_SMS.Infrastructure.IRepository;
using Prabin_SMS.Models.Entity;

namespace Prabin_SMS.web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICRUDServices<Course> _course;
        private readonly ICRUDServices<Student> _student;
        private readonly ICRUDServices<StudentCourse> _studentCourse;

        public async Task<IActionResult> Index()
        {
            var course =await _course.GetAllAsync();
            ViewBag.student = _student.GetAllAsync();


            return View(course);
        }


    }
}
