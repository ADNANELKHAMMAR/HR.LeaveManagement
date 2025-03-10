using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HRLeaveManagementDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _dbContext.leaveAllocations.AddRangeAsync(allocations);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string UserId, int LeaveTypeId, int Period)
        {
            return await _dbContext.leaveAllocations
                             .AnyAsync(l => l.LeaveTypeId == LeaveTypeId
                                        && l.Period == Period
                                        && l.EmployeeId == UserId);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string UserId)
        {
            var AllocationsByUser = await _dbContext.leaveAllocations
                                              .Include(l=>l.LeaveType)
                                              .Where(l => l.EmployeeId == UserId)
                                              .ToListAsync();
            return AllocationsByUser;
            
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var Allocations = await _dbContext.leaveAllocations
                                              .Include(l => l.LeaveType)
                                              .ToListAsync();
            return Allocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var Allocation = await _dbContext.leaveAllocations
                                              .Include(l => l.LeaveType)
                                              .FirstOrDefaultAsync(l=>l.Id == id);
            return Allocation;
        }

        public async Task<LeaveAllocation> GetUserAllocation(string UserId, int LeaveTypeId)
        {
            var Allocation = await _dbContext.leaveAllocations
                                              .FirstOrDefaultAsync(l => l.EmployeeId == UserId
                                                                    &&l.LeaveTypeId == LeaveTypeId);
            return Allocation;
        }
    }
}
