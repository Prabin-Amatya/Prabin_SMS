using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prabin_SMS.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Infrastructure.Entity_Configuration
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.ProfileUrl)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(p => p.CreatedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()")
               .HasColumnType("DATETIME");

            builder.Property(p => p.ModifiedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()")
               .HasColumnType("DATETIME");

            builder.Property(p => p.ProfileUrl)
               .IsRequired(false);

            builder.HasMany(e => e.Courses)
              .WithMany(e => e.Teachers)
              .UsingEntity<TeacherCourse>(
              l => l.HasOne(e => e.Course).WithMany(e => e.TeacherCourses).HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.Restrict),
              r => r.HasOne(e => e.Teacher).WithMany(e => e.TeacherCourses).HasForeignKey(e => e.TeacherId).OnDelete(DeleteBehavior.Restrict)
              );

            builder.HasMany(e => e.Degrees)
              .WithMany(e => e.Teachers)
              .UsingEntity<TeacherDegree>(
              l => l.HasOne(e => e.Degree).WithMany(e => e.TeacherDegrees).HasForeignKey(e => e.DegreeId).OnDelete(DeleteBehavior.Restrict),
              r => r.HasOne(e => e.Teacher).WithMany(e => e.TeacherDegrees).HasForeignKey(e => e.TeacherId).OnDelete(DeleteBehavior.Restrict)
              );

            builder.HasMany(e => e.Students)
              .WithMany(e => e.Teachers)
              .UsingEntity<TeacherStudent>(
              l => l.HasOne(e => e.Student).WithMany(e => e.TeacherStudents).HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Restrict),
              r => r.HasOne(e => e.Teacher).WithMany(e => e.TeacherStudents).HasForeignKey(e => e.TeacherId).OnDelete(DeleteBehavior.Restrict)
              );
        }
    }
}
