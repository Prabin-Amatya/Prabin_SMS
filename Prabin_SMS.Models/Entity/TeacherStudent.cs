using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class TeacherStudent:BaseEntity
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
    }
}