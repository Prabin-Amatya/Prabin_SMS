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
        public IEnumerable<Student> Students{  get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; }

    }
}
