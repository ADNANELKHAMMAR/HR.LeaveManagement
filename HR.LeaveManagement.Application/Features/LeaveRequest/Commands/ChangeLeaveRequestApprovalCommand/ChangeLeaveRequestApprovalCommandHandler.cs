using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApprovalCommand
{
    public class ChangeLeaveRequestApprovalCommandHandler :
        IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository requestRepository,
                                                IEmailSender emailSender,
                                                IMapper mapper)
        {
            _leaveRequestRepository = requestRepository;
            this._emailSender = emailSender;
            this._mapper = mapper;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }
            leaveRequest.Approved = request.Approved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);
            //if request was approved , update the employee's allocations
            try
            {
                var Email = new EmailMessage
                {
                    Body = $"Your leave Request Approval for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated successfully",
                    Subject = "Leave Request Approval updated",
                    To = string.Empty //Get email from Employee record
                };
                await _emailSender.SendEmailAsync(Email);
            }
            catch (Exception ex)
            {
                // _appLogger.LogWarning(ex.Message);
            }
            return Unit.Value;
        }
    }
}
