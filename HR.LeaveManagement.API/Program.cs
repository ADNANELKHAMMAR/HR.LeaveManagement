using HR.LeaveManagement.API.Middleware;
using HR.LeaveManagement.Application;
using HR.LeaveManagement.Infrastructure;
using HR.LeaveManagement.Persistence;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.GetApplicationServices();
builder.Services.GetPersistenceServices(builder.Configuration);
builder.Services.GetInfrastructureServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("all", conf => conf.AllowAnyHeader()
                                         .AllowAnyMethod()
                                         .AllowAnyOrigin());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("all");
app.UseHttpsRedirection();  
app.UseRouting();          
app.UseAuthorization(); 
app.MapControllers();
app.Run();
