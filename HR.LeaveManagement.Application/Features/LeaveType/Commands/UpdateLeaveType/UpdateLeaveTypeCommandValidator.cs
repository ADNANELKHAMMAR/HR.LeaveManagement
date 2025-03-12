using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this._leaveTypeRepository = leaveTypeRepository;
            RuleFor(x => x.Id).NotNull()
                            .MustAsync(async(int Id,CancellationToken token) =>
                            {
                                return await _leaveTypeRepository.GetByIdAsync(Id) != null;
                            }).WithMessage("Invalid Id: Leave type does not exist."); 
            RuleFor(x => x.Name).NotEmpty().WithMessage("{propertyName} is required")
                                .NotNull()
                                .MaximumLength(20).WithMessage("{propertyName} with max length of 20");
            RuleFor(x => x).MustAsync(LeaveTypeNameUnique)
                           .WithMessage("a leavetype with this name already exists");

        }
        private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand updateLeaveType, CancellationToken token)
        {
            return await _leaveTypeRepository.isLeaveTypeUnique(updateLeaveType.Name);
        }
    }
}
