using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeDetailQueryTestsHandler
    {
        private readonly Mock<ILeaveTypeRepository> _MockRepo;
        private readonly IMapper _mapper;
        public GetLeaveTypeDetailQueryTestsHandler()
        {
            _MockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConf = new MapperConfiguration(c =>
                {
                    c.AddProfile<LeaveTypeProfile>();
                });
            _mapper = mapperConf.CreateMapper();
        }
        [Fact]
        public async Task GetLeaveTypeById()
        {
            var handler = new GetLeaveTypeDetailsQueryHandler(_MockRepo.Object, _mapper);
            var GetLeaveTypeDetailsQuery = new GetLeaveTypeDetailsQuery(1);
            var result = await handler.Handle(GetLeaveTypeDetailsQuery, CancellationToken.None);
            result.ShouldBeOfType<LeaveTypeDetailsDto>();
            result.ShouldNotBeNull();
            
        }
    }
}
