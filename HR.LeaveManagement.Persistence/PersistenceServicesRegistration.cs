using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection GetPersistenceServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<HRLeaveManagementDbContext>(opt =>
                    opt.UseSqlServer(config.GetConnectionString("HRLeaveDBConnectionString")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILeaveTypeRepository,LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository,LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository,LeaveAllocationRepository>();
            return services;
        }
    }
}
