using Prabin_SMS.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.ViewModels
{
    public class StudentViewModel
    {
        public StudentSearchViewModel SearchViewModel { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
