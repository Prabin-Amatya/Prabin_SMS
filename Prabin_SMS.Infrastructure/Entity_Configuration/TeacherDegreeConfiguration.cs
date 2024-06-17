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
    public class TeacherDegreeConfiguration : IEntityTypeConfiguration<TeacherDegree>
    {
        public void Configure(EntityTypeBuilder<TeacherDegree> builder)
        {
            builder.HasAlternateKey(p => new { p.TeacherId, p.DegreeId});
            
        }
    }
}