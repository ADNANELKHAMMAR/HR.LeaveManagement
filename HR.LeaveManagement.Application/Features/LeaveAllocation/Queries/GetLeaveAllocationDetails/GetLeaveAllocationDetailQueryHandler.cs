using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailQueryHandler : IRequestHandler<GetLeaveAllocationDetailQuery, LeaveAllocationDetailDto>
    {
        private readonly ILeaveAllocationRepository _leaveAllocation;
        private readonly IMapper _mapper;

        public GetLeaveAllocationDetailQueryHandler(ILeaveAllocationRepository leaveAllocation, IMapper mapper)
        {
            this._leaveAllocation = leaveAllocation;
            _mapper = mapper;
        }
        public async Task<LeaveAllocationDetailDto> Handle(GetLeaveAllocationDetailQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocation.GetLeaveAllocationWithDetails(request.id);
            return _mapper.Map<LeaveAllocationDetailDto>(leaveAllocation);
        }
    }
}
