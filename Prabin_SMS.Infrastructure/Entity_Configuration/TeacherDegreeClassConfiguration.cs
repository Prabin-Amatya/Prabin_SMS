﻿using Microsoft.EntityFrameworkCore;
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
    public class TeacherDegreeClassConfiguration : IEntityTypeConfiguration<TeacherDegreeClass>
    {
        public void Configure(EntityTypeBuilder<TeacherDegreeClass> builder)
        {
            builder.HasAlternateKey(p => new { p.TeacherDegreeId, p.TeacherClassId });
        }
    }
}