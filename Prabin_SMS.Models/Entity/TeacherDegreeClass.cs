using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class TeacherDegreeClass:BaseEntity
    {
        public int TeacherClassId { get; set; }
        public int TeacherDegreeId { get; set; }

        public TeacherClass TeacherClass { get; set; }
        public TeacherDegree TeacherDegree { get; set; }

    }
}
