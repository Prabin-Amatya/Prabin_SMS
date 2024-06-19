using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Prabin_SMS.Infrastructure.IRepository;
using Prabin_SMS.Models.Entity;
using Prabin_SMS.Models.ViewModels;
using Prabin_SMS.web.Models;

namespace Prabin_SMS.web.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ICRUDServices<Course> _course;
        private readonly ICRUDServices<Student> _student;
        private readonly ICRUDServices<Degree> _degree;
        private readonly UserManager<ApplicationUser> _user;

        public StudentController(ICRUDServices<Course> course, ICRUDServices<Student> student, UserManager<ApplicationUser> user, ICRUDServices<Degree> degree)
        {
            _course = course;
            _student = student;
            _user = user;
            _degree = degree;
        }

        public async Task<IActionResult> Index()
        {
            StudentViewModel studentViewModel = new StudentViewModel();
            ViewBag.DegreeList = await _degree.GetAllAsync();
            
            studentViewModel.Students = await _student.GetAllAsync();
            return View(studentViewModel);
        }

        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.DegreeList = await _degree.GetAllAsync();

            Student student = new Student();
            if (id != 0)
            {
                student = await _student.GetAsync(id);
            }
            return View(student);

        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(Student student)
        {
            if (ModelState.IsValid)
            {
                ViewBag.DegreeList = await _degree.GetAllAsync();
                var UserId = _user.GetUserId(HttpContext.User);
                var user =await _user.FindByIdAsync(UserId);

                if (student.studentPhoto != null)
                {
                    string fileDirectory = $"wwwroot/StudentImage";

                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    string uniqueFileName = Guid.NewGuid() + "_" + student.studentPhoto.FileName;
                    string filePath = Path.Combine(Path.GetFullPath($"wwwroot/StudentImage"), uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await student.studentPhoto.CopyToAsync(fileStream);
                        student.studenturl = $"StudentImage/" + uniqueFileName;
                    }

                }

                if (student.transcriptPhoto != null)
                {
                    string fileDirectory = $"wwwroot/TranscriptImage";

                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    string uniqueFileName = Guid.NewGuid() + "_" + student.transcriptPhoto.FileName;
                    string filePath = Path.Combine(Path.GetFullPath($"wwwroot/TranscriptImage"), uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await student.transcriptPhoto.CopyToAsync(fileStream);
                        student.transcriptPhotoUrl = $"TranscriptImage/" + uniqueFileName;
                    }
                }

                if (student.citizenshipPhoto != null)
                {
                    string fileDirectory = $"wwwroot/citizenshipPhotoImage";

                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    string uniqueFileName = Guid.NewGuid() + "_" + student.citizenshipPhoto.FileName;
                    string filePath = Path.Combine(Path.GetFullPath($"wwwroot/citizenshipPhotoImage"), uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await student.citizenshipPhoto.CopyToAsync(fileStream);
                        student.citizenshipPhotoUrl = $"citizenshipPhotoImage/" + uniqueFileName;
                    }

                }

                if (student.Id == 0)
                {
                    student.CreatedBy = UserId;
                    student.CreatedDate = DateTime.Now;
                    await _student.InsertAsync(student);
                }
                else if (student.Id != 0)
                {
                    Student updated_student = await _student.GetAsync(student.Id);
                    updated_student.FirstName = student.FirstName;
                    updated_student.LastName = student.LastName;
                    updated_student.Email = student.Email;
                    updated_student.Address = student.Address;
                    updated_student.PhoneNumber = student.PhoneNumber;
                    updated_student.IsEnrolled = student.IsEnrolled;
                    updated_student.Batch = student.Batch;
                    updated_student.SectionId = student.SectionId;
                    updated_student.Semester = student.Semester;
                    updated_student.ModifiedDate = DateTime.Now;
                    updated_student.ModifiedBy = UserId;
                    updated_student.DegreeId = student.DegreeId;
                    await _student.UpdateAsync(updated_student);
                    user.HasEnrolled = true;
                    await _user.UpdateAsync(user);
                }

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(AddEdit));

        
        }

        public async Task<IActionResult> EnrollmentStatus(int Id)
        {
            var student = await _student.GetAsync(Id);
            if (student.IsEnrolled == true)
            {
                student.IsEnrolled = false;
            }
            else
            {
                student.IsEnrolled = true;
                var degree = await _degree.GetAsync(student.DegreeId);
                if (student.Semester==0)
                {
                    student.Semester = 1;
                }
            }
            await _student.UpdateAsync(student);
            return RedirectToAction(nameof(Index));

        }


    }
}
