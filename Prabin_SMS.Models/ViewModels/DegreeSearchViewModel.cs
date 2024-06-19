using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.ViewModels
{
    public class DegreeSearchViewModel
    {
        public string Query {  get; set; }
        public int CourseId { get; set; }
        public string Level {  get; set; }
        public int DisciplineId { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
