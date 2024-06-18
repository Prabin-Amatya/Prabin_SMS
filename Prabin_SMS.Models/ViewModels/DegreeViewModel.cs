using Prabin_SMS.Models.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.ViewModels
{
    public class DegreeViewModel
    {
        public IEnumerable<DegreeCourse> DegreeCourse {  get; set; }
        public Degree Degree {  get; set; }
    }
}
