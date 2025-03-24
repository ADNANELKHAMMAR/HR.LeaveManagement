using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>

    {
        private readonly ILeaveTypeRepository _leaveType;
        public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveType)
        {
            this._leaveType = leaveType;
            RuleFor(q => q.leaveTypeId).MustAsync(LeaveTypeMustExist)
                                     .WithMessage("{PropertyName} deos not exist");
        }
        private async Task<bool> LeaveTypeMustExist(int id , CancellationToken token)
        {
            var leaveType = await _leaveType.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
