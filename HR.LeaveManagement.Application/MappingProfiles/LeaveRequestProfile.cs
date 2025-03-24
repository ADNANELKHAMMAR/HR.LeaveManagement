using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApprovalCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequest,LeaveRequestListDto>();
            CreateMap<LeaveRequest,LeaveRequestDetailDto>();
            CreateMap<LeaveRequest,CancelLeaveRequestCommand>();
            CreateMap<LeaveRequest,UpdateLeaveRequestCommand>();
            CreateMap<LeaveRequest,DeleteLeaveRequestCommand>();
            CreateMap<LeaveRequest,ChangeLeaveRequestApprovalCommand>();

        }
    }
}
