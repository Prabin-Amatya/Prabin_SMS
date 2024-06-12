using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }

        public Student StudentId {  get; set; }
        public IEnumerable<Student> Students{  get; set; }

    }
}
