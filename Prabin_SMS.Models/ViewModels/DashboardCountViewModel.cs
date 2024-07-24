using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.ViewModels
{
    [Keyless]
    public class DashboardCountViewModel
    {
        public string DegreeName {  get; set; }
        public int Enrollments {  get; set; }
    }
}
