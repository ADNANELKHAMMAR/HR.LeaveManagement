using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetLeaveAllocationsQueryHandler
        : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocation;
        private readonly IMapper _mapper;

        public GetLeaveAllocationsQueryHandler(ILeaveAllocationRepository leaveAllocation,IMapper mapper)
        {
            this._leaveAllocation = leaveAllocation;
            _mapper = mapper;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
        {
            var LeaveAllocations =await _leaveAllocation.GetLeaveAllocationsWithDetails();
            return _mapper.Map<List<LeaveAllocationDto>>(LeaveAllocations);
        }
    }
}
