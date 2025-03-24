using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailDto>
    {
        private readonly ILeaveRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository requestRepository, IMapper mapper)
        {
            this._requestRepository = requestRepository;
            this._mapper = mapper;
        }
        public async Task<LeaveRequestDetailDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        {
            var LeaveRequest = await _requestRepository.GetLeaveRequestWithDetails(request.id);
            if (LeaveRequest == null)
            {
                throw new NotFoundException(nameof(LeaveRequest),request.id);
            }
            return _mapper.Map<LeaveRequestDetailDto>(LeaveRequest);
        }
    }
}
