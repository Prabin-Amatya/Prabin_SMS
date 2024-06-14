using Microsoft.EntityFrameworkCore;
using Prabin_SMS.Infrastructure.Entity_Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Infrastructure
{
    public class SMSDbContext:DbContext
    {
        public SMSDbContext(DbContextOptions<SMSDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new StudentCourseConfiguration());
        }
    }
}
