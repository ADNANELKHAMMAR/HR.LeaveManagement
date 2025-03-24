using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequestCommand
{
    public class CreateLeaveRequestCommandHandler :
        IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository _requestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository requestRepository,
                                                ILeaveTypeRepository leaveTypeRepository,
                                                IMapper mapper,
                                                IEmailSender emailSender,
                                                IAppLogger<CreateLeaveRequestCommandHandler> appLogger)
        {
            _requestRepository = requestRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
            this._emailSender = emailSender;
            this._appLogger = appLogger;
        }
        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator( _leaveTypeRepository);
            var ValidationResult = await validator.ValidateAsync(request);
            if (!ValidationResult.IsValid)
            {
                throw new BadRequestException("Invalid Leave Request", ValidationResult);
            }
            //Get requesting Employee id
            //Check on employee allocations
            //create leave Request
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            await _requestRepository.CreateAsync(leaveRequest);
            //Send Mail
            try
            {
                var Email = new EmailMessage
                {
                    Body = $"Your leave Request for {request.StartDate:D} to {request.EndDate:D} has been created successfully",
                    Subject = "Leave Request created",
                    To = string.Empty //Get email from Employee record
                };
                await _emailSender.SendEmailAsync(Email);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
            }
            return leaveRequest.Id;
        }
    }
}
