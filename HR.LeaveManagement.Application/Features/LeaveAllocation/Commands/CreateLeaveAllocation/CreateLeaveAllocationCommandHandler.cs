using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocation;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveType;
        private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _appLogger;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocation,
                                                   IMapper mapper,ILeaveTypeRepository leaveType,
                                                   IAppLogger<CreateLeaveAllocationCommandHandler> appLogger)
        {
            this._leaveAllocation = leaveAllocation;
            _mapper = mapper;
            this._leaveType = leaveType;
            this._appLogger = appLogger;
        }
        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveType);
            var validationResult =await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _appLogger.LogWarning(validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException("Invalid leave allocation Request",validationResult);
            }
            var leaveType = await _leaveType.GetByIdAsync(request.leaveTypeId);
            //Get Employyes
            //Get Period
            //Assign Allocations
            var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
            await _leaveAllocation.CreateAsync(leaveAllocation);
            return Unit.Value;

        }
    }
}
