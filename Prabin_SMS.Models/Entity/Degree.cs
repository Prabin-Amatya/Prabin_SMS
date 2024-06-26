﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Models.Entity
{
    public class Degree:BaseEntity
    {
        public string DegreeName { get; set; }
        public string DegreeDescription {  get; set; }
        public string Academic_Level { get; set; }
        [Display(Name = "Number Of Semesters:")]
        public int No_Of_Semesters {  get; set; }
        [Display(Name = "Number Of Years:")]
        public int No_Of_Years { get; set; }
        public bool IsActive { get; set; }
        public int TotalSeats {  get; set; }
        [Column(TypeName="DATE")]
        public DateTime StartDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public virtual Discipline Discipline { get; set; }
        public int DisciplineId { get; set; }
        public virtual IEnumerable<Student> Students { get; set; }
        public virtual IEnumerable<Course> Courses { get; set; }
        public virtual IEnumerable<DegreeCourse> DegreeCourses { get; set; }
    }
}
