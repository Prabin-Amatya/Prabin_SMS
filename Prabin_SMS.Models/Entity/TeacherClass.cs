using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class TeacherClass : BaseEntity
    {
        public int? Semester { get; set; }
        public int? Section { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public IEnumerable<TeacherDegree> TeacherDegrees {get; set;}
        public IEnumerable<TeacherDegreeClass> TeacherDegreeClasses { get; set; }
    }
}
