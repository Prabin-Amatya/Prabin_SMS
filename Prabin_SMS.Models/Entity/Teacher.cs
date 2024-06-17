using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string? ProfileUrl { get; set; }
        [NotMapped]
        public IFormFile ProfilePicture { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public int TeacherDegreeCourseId { get; set; }
        public IEnumerable<Degree> Degrees { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<TeacherCourse> TeacherCourses { get; set;}
        public IEnumerable<TeacherStudent> TeacherStudents { get; set; }
        public IEnumerable<TeacherDegree> TeacherDegrees { get; set; }
    }
}
