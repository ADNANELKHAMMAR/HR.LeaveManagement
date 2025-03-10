using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string UserId);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        Task<bool> AllocationExists(string UserId , int LeaveTypeId ,int Period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocation(string UserId, int LeaveTypeId);
    }
}
