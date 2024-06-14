using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class Student:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Class {  get; set; }
        public string Section {  get; set; }
        
        public string studenturl {  get; set; }

        [NotMapped]
        public IFormFile studentPhoto {  get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; }
    }
}
