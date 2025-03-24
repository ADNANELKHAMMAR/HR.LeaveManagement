using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests
{
    public class GetAllLeaveRequestsQueryHandler :
        IRequestHandler<GetAllLeaveRequestsQuery, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public GetAllLeaveRequestsQueryHandler(ILeaveRequestRepository requestRepository,IMapper mapper) 
        {
            this._requestRepository = requestRepository;
            this._mapper = mapper;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetAllLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            var LeavAllocations = await _requestRepository.GetAllAsync();
            return _mapper.Map<List<LeaveRequestListDto>>(LeavAllocations);
        }
    }
}
