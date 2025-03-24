using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler :
        IRequestHandler<DeleteLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocation;
        private readonly IAppLogger<DeleteLeaveAllocationCommandHandler> _appLogger;

        public DeleteLeaveAllocationCommandHandler
            (ILeaveAllocationRepository leaveAllocation,
            IAppLogger<DeleteLeaveAllocationCommandHandler> appLogger)
        {
            this._leaveAllocation = leaveAllocation;
            this._appLogger = appLogger;
        }
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteLeaveAllocationValidator(_leaveAllocation);
            var resultValidation = await validator.ValidateAsync(request);
            if(!resultValidation.IsValid)
            {
                var errorMessage = resultValidation.Errors.First().ErrorMessage;
                _appLogger.LogWarning(errorMessage);
                throw new BadRequestException(errorMessage);
            }
            var leaveAllocation = await _leaveAllocation.GetByIdAsync(request.Id);
            await _leaveAllocation.DeleteAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
