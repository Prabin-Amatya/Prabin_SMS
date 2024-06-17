using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class Course:BaseEntity
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int CreditHours { get; set; }
        public int FullMarks { get; set; }
        public bool IsActive { get; set; }
        public bool IsOptional { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public IEnumerable<Student> Students{  get; set; }
        public IEnumerable<Degree> Degrees { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; }
        public IEnumerable<DegreeCourse> DegreeCourses { get; set; }
        public IEnumerable<TeacherCourse> TeacherCourses { get; set; }

    }
}
