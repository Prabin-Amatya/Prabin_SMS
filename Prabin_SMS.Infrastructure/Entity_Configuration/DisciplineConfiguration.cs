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
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CreatedDate)
             .IsRequired()
             .HasDefaultValueSql("GETDATE()")
             .HasColumnType("DATETIME");

            builder.Property(p => p.ModifiedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()")
               .HasColumnType("DATETIME");

            builder.HasMany(e => e.Degrees)
                 .WithOne(e => e.Discipline)
                 .HasForeignKey(e => e.DisciplineId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
