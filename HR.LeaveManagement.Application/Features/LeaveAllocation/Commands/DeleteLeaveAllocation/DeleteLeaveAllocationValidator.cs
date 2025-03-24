using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationValidator:
        AbstractValidator<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _allocationRepository;

        public DeleteLeaveAllocationValidator(ILeaveAllocationRepository allocationRepository)
        {
            _allocationRepository = allocationRepository;
            RuleFor(p => p.Id).NotNull()
                              .MustAsync(LeaveAllocationMustExist)
                              .WithMessage("{PropertyName} must be present");
        }
        private async Task<bool> LeaveAllocationMustExist(int Id, CancellationToken token)
        {
            var leaveAllocation = await _allocationRepository.GetByIdAsync(Id);
            return leaveAllocation != null;
        }
    }
}
