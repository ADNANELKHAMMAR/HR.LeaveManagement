using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType 
                { 
                    Id = 1, 
                    DefaultDays = 10, 
                    Name = "Vacation",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
                );
            // DataBase Level restriction 
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.DefaultDays).IsRequired();
        }
    }
}
