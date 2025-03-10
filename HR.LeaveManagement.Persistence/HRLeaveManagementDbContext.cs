using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence
{
    public class HRLeaveManagementDbContext : DbContext
    {
        public HRLeaveManagementDbContext(DbContextOptions<HRLeaveManagementDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRLeaveManagementDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override  Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            foreach(var entry in base.ChangeTracker.Entries<BaseEntity>().
                Where(e=>e.State ==EntityState.Added ||e.State ==EntityState.Modified ))
            {
                entry.Entity.DateModified = DateTime.Now;
                if(entry.State == EntityState.Added)
                    entry.Entity.DateCreated = DateTime.Now;
                
            }
            return  base.SaveChangesAsync(token);
        }
        public DbSet<LeaveRequest> leaveRequests { get; set; }
        public DbSet<LeaveType> leaveTypes { get; set; }
        public DbSet<LeaveAllocation> leaveAllocations { get; set; }
    }
}
