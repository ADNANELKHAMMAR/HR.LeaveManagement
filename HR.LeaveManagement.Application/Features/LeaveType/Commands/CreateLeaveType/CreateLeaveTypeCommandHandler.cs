using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, 
                                             IMapper mapper, IAppLogger<CreateLeaveTypeCommandHandler> logger)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            this._logger = logger;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validationResult =await validator.ValidateAsync(request);
            string Errors="";
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors) 
                {
                    Errors += error.ErrorMessage + "\n";
                }
                _logger.LogWarning("Validation error(s) while creating {0}  :\n {1}",nameof(LeaveType),Errors);
                throw new BadRequestException("Invalid LeaveType",validationResult);
            }
            var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);
             await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);
            return leaveTypeToCreate.Id;

        }
    }
}
