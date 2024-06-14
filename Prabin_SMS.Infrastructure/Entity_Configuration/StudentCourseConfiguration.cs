using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prabin_SMS.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;


namespace Prabin_SMS.Infrastructure.Entity_Configuration
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(p => new { p.StudentId, p.CourseId });
            //builder.HasOne(e=>e.Course)
            //    .WithMany(e=>e.StudentCourses)
            //    .HasForeignKey(e=>e.CourseId)
            //    .OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(e => e.Student)
            //    .WithMany(e => e.StudentCourses)
            //    .HasForeignKey(e => e.StudentId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
