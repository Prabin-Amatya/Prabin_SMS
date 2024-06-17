using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class TeacherCourse:BaseEntity
    {
        public int TeacherId {  get; set; }
        public int CourseId { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }


    }
}
