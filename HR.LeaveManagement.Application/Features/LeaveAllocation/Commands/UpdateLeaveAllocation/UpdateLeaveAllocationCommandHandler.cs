using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocation;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveType;
        private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _appLogger;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocation,
                                                   IMapper mapper, ILeaveTypeRepository leaveType,
                                                   IAppLogger<UpdateLeaveAllocationCommandHandler> appLogger)
        {
            this._leaveAllocation = leaveAllocation;
            _mapper = mapper;
            this._leaveType = leaveType;
            this._appLogger = appLogger;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveType,_leaveAllocation);
            var result = await validator.ValidateAsync(request);
            if (result.IsValid)
            {
                throw new BadRequestException("Invalid LeaveAllocation" , result);
            }
            var leaveAllocation = await _leaveAllocation.GetByIdAsync(request.Id);
            _mapper.Map(request, leaveAllocation);
            await _leaveAllocation.UpdateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
