﻿
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Infrastructure.EmailService;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Infrastructure.Logging;

namespace HR.LeaveManagement.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static  IServiceCollection GetPersistenceServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IAppLogger<>),typeof(LoggerAdapter<>));
            return services;
        }
    }
}
