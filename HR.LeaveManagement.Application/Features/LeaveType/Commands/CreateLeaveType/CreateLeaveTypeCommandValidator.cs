using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            RuleFor(x => x.DefaultDays).NotEmpty()
                                     .NotNull().WithMessage("{propertyName} is required")
                                     .GreaterThan(0).WithMessage("{propertyName} should be  greater than 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("{propertyName} is required")
                                .MaximumLength(20).WithMessage("{propertyName} with max length of 20");
            RuleFor(x=>x).MustAsync(LeaveTypeNameUnique);
            
        }
        private async Task<bool>  LeaveTypeNameUnique(CreateLeaveTypeCommand createLeaveType,CancellationToken token)
        {
            return await _leaveTypeRepository.isLeaveTypeUnique(createLeaveType.Name);
        }
    }
}
