using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HRLeaveManagementDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> isLeaveTypeUnique(string Name)
        {
            return !(await _dbContext.leaveTypes.AnyAsync(x => x.Name == Name));
        }
    }
}
