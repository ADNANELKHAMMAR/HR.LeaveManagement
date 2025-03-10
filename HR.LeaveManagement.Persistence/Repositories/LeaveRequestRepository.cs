using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HRLeaveManagementDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            return await _dbContext.leaveRequests
                                   .Include(l=>l.LeaveType)
                                   .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string UserId)
        {
            return await _dbContext.leaveRequests
                        .Where(t=>t.RequestingEmployeeId == UserId)
                        .Include(l => l.LeaveType)
                        .ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int Id)
        {
            return await _dbContext.leaveRequests
                                   .Include(l => l.LeaveType)
                                   .FirstOrDefaultAsync(t => t.Id == Id);
                        
        }
    }
}
