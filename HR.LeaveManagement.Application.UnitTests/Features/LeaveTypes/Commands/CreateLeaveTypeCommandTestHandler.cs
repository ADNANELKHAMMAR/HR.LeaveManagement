using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandTestHandler
    {
        private readonly Mock<ILeaveTypeRepository> _MockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<CreateLeaveTypeCommandHandler>> _appLogger;
        public CreateLeaveTypeCommandTestHandler()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _MockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            _appLogger = new Mock<IAppLogger<CreateLeaveTypeCommandHandler>>();
        }
        [Fact] 
        private async Task CreateLeaveTypeTest()
        {
            var handler = new CreateLeaveTypeCommandHandler(_MockRepo.Object, _mapper, _appLogger.Object);
            var request = new CreateLeaveTypeCommand() { DefaultDays = 5, Name = "TestSick" };
            var result =await  handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
            var leaveTypes =await _MockRepo.Object.GetAllAsync();
            leaveTypes.Count.ShouldBe(4);

        }
    }
}
