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
    public class TeacherClassConfiguration : IEntityTypeConfiguration<TeacherClass>
    {
        public void Configure(EntityTypeBuilder<TeacherClass> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Semester)
                .IsRequired(false);

            builder.Property(p => p.Section)
                .IsRequired(false);

            builder.Property(p => p.StartTime)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.StartTime)
            .HasColumnType("TIME");

            builder.Property(p => p.EndTime)
            .HasColumnType("TIME");


            builder.HasMany(e => e.TeacherDegrees)
              .WithMany(e => e.TeacherClasses)
              .UsingEntity<TeacherDegreeClass>(
              l => l.HasOne(e => e.TeacherDegree).WithMany(e => e.TeacherDegreeClasses).HasForeignKey(e => e.TeacherDegreeId).OnDelete(DeleteBehavior.Restrict),
              r => r.HasOne(e => e.TeacherClass).WithMany(e => e.TeacherDegreeClasses).HasForeignKey(e => e.TeacherClassId).OnDelete(DeleteBehavior.Restrict)
              );

        }
    }
}
