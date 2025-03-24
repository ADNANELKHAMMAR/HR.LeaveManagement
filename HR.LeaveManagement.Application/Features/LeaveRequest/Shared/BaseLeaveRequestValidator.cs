using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository _leaveType;

        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveType)
        {
            this._leaveType = leaveType;
            RuleFor(p => p.StartDate).LessThan(p => p.EndDate)
                                   .WithMessage("{PropertyName} must be before {ComparisonName}");
            RuleFor(p => p.EndDate).LessThan(p => p.StartDate)
                                   .WithMessage("{PropertyName} must be after {ComparisonName}");
            RuleFor(p => p.LeaveTypeId).NotNull()
                                   .GreaterThan(0)
                                   .MustAsync(LeaveTypeMustExist)
                                   .WithMessage("{PropertyName} Does not exist");

        }

        private async Task<bool> LeaveTypeMustExist(int LeaveTypeId, CancellationToken token)
        {
            var leave = await _leaveType.GetByIdAsync(LeaveTypeId);
            return leave != null;
        }
    }
}
