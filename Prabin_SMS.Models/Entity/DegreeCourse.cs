using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class DegreeCourse : BaseEntity
    {
        public int DegreeId { get; set; }
        public int CourseId { get; set; }
        public int Semester {  get; set; }
        public Degree Degree { get; set; }
        public Course Course { get; set; }
    }
}
