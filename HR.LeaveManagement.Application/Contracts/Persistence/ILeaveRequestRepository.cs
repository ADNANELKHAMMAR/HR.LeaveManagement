using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string SuerId);
        Task<LeaveRequest> GetLeaveRequestWithDetails(int Id);
    }
}
