using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _requestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository requestRepository,
                                                ILeaveTypeRepository leaveTypeRepository,
                                                IMapper mapper,
                                                IEmailSender emailSender ,
                                                IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
        {
            _requestRepository = requestRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
            this._emailSender = emailSender;
            this._appLogger = appLogger;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveRequestCommandValidator(_requestRepository, _leaveTypeRepository);
            var ValidationResult =await validator.ValidateAsync(request);
            if (!ValidationResult.IsValid) 
            {
                throw new BadRequestException("Invalid Leave Request", ValidationResult);
            }
            var leaveRequest = await _requestRepository.GetByIdAsync(request.Id);
            _mapper.Map(request, leaveRequest);
            await _requestRepository.UpdateAsync(leaveRequest);
            //Send Mail
            try
            {
                var Email = new EmailMessage
                {
                    Body = $"Your leave Request for {request.StartDate} to {request.EndDate} has been updated successfully",
                    Subject = "Leave Request submitted",
                    To = string.Empty //Get email from Employee record
                };
                await _emailSender.SendEmailAsync(Email);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
            }
            return Unit.Value;

        }
    }
}
