using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class TeacherDegree:BaseEntity
    {
        public int TeacherId { get; set; }
        public int DegreeId { get; set; }
        
        public Teacher Teacher { get; set; }
        public Degree Degree { get; set; }

        public IEnumerable<TeacherClass> TeacherClasses { get; set; }
        public IEnumerable<TeacherDegreeClass> TeacherDegreeClasses { get; set; }


    }
}