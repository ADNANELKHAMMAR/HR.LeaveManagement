using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequestCommand;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequestCommand
{
    public class CancelLeaveRequestCommandHandler :
        IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository requestRepository,
                                                IEmailSender emailSender)
        {
            _leaveRequestRepository = requestRepository;
            this._emailSender = emailSender;
        }
        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest =await _leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }
            leaveRequest.Cancelled = true;
            //Re-Evaluate the employee's allocations for the leave type 
            //Send email cinfirmation
            try
            {
                var Email = new EmailMessage
                {
                    Body = $"Your leave Request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled successfully",
                    Subject = "Leave Request cancelled",
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
