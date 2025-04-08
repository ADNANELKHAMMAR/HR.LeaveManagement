using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            var leaveTypes = new List<Domain.LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test Sick"
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 13,
                    Name = "Test Maternity"
                }
            };
            var moqRepo = new Mock<ILeaveTypeRepository>();
            moqRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveTypes);
            moqRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id)=>
            {
                return leaveTypes.Find(a => a.Id == id);
            }
            );
            moqRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                   .Returns((LeaveType leavetype) =>
                   {
                       leaveTypes.Add(leavetype);
                       return Task.FromResult(leavetype);
                   });
            moqRepo.Setup(r => r.isLeaveTypeUnique(It.IsAny<string>()))
                .Returns((string Name) =>
                {
                    return Task.FromResult(!leaveTypes.Any(a => a.Name == Name));
                });
            return moqRepo;
        }
    }
}
