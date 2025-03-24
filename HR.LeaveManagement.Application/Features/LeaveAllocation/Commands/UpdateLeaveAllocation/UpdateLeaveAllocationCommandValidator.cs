using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository _leaveType;
        private readonly ILeaveAllocationRepository _allocationRepository;

        public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveType,ILeaveAllocationRepository allocationRepository)
        {
            this._leaveType = leaveType;
            this._allocationRepository = allocationRepository;
            RuleFor(p => p.NumberOfDays).GreaterThanOrEqualTo(DateTime.Now.Year)
                                      .WithMessage("{PropertyName} must greater than {ComparisonValue}");
            RuleFor(p => p.Period).GreaterThanOrEqualTo(DateTime.Now.Year)
                                      .WithMessage("{PropertyName} must be {ComparisonValue} or greater");
            RuleFor(p => p.LeaveTypeId).MustAsync(LeaveTypeMustExist)
                                     .WithMessage("{PropertyName} deos not exist");
            RuleFor(p => p.Id).NotNull()
                              .MustAsync(LeaveAllocationMustExist)
                              .WithMessage("{PropertyName} must be present");
        }

        private async Task<bool> LeaveAllocationMustExist(int Id, CancellationToken token)
        {
            var leaveAllocation = await _allocationRepository.GetByIdAsync(Id);
            return leaveAllocation != null;
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            var leaveType = await _leaveType.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
