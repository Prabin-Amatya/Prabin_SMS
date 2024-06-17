﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Prabin_SMS.Infrastructure.IRepository;
using Prabin_SMS.Models.Entity;
using Prabin_SMS.web.Models;

namespace Prabin_SMS.web.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ICRUDServices<Course> _course;
        private readonly ICRUDServices<Student> _student;
        private readonly ICRUDServices<Degree> _degree;
        private readonly ICRUDServices<Teacher> _teacher;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ICRUDServices<StudentCourse> _studentCourse;

        public StudentController(ICRUDServices<Course> course, ICRUDServices<Student> student, UserManager<ApplicationUser> user, ICRUDServices<StudentCourse> studentCourse, ICRUDServices<Degree> degree, ICRUDServices<Teacher> teacher)
        {
            _course = course;
            _student = student;
            _user = user;
            _studentCourse = studentCourse;
            _degree = degree;
            _teacher = teacher;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.DegreeList = await _degree.GetAllAsync();
            
            var student = await _student.GetAllAsync();
            return View(student);
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
            ViewBag.DegreeList = await _degree.GetAllAsync();
            var UserId = _user.GetUserId(HttpContext.User);
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
            }

            return RedirectToAction(nameof(Index));

        }


    }
}
