using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Class {  get; set; }
        public string Section {  get; set; }
        
        public string studenturl {  get; set; }
        public IFormFile studentPhoto {  get; set; }
        public int CourseId {  get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
