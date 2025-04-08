using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shouldly;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HR.LeaveManagement.Application.IntegrationTests
{
    public class HrDatabaseContextTest
    {
        private readonly HRLeaveManagementDbContext _HRLeaveManagementDbContext;

        public HrDatabaseContextTest()
        {
            var dbOptions =new DbContextOptionsBuilder<HRLeaveManagementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _HRLeaveManagementDbContext = new HRLeaveManagementDbContext(dbOptions);
            
        }
        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            //Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                Name = "Test Sick",
                DefaultDays = 5
            };
            //Act
           await  _HRLeaveManagementDbContext.leaveTypes.AddAsync(leaveType);
           await  _HRLeaveManagementDbContext.SaveChangesAsync();
            //Assert
            leaveType.DateCreated.ShouldNotBeNull();
        }
        
    }
}