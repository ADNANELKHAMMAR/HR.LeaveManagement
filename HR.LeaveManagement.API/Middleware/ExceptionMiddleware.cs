using AutoMapper.Internal;
using HR.LeaveManagement.API.Models;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace HR.LeaveManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails Problem = new();
            switch(ex)
            {
                case BadRequestException badRequest:
                    statusCode = HttpStatusCode.BadRequest;
                    Problem = new CustomProblemDetails
                    {
                        Title = badRequest.Message,
                        Status = (int)statusCode,
                        Detail = badRequest.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequest.ValidationErrors
                    };

                    break;
                case NotFoundException notFound:
                    statusCode = HttpStatusCode.NotFound;
                    Problem = new CustomProblemDetails
                    {
                        Title = notFound.Message,
                        Status = (int)statusCode,
                        Detail = notFound.InnerException?.Message,
                        Type = nameof(NotFoundException)
                    };
                    break;
                default:
                    Problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Detail = ex.StackTrace,
                        Type = nameof(HttpStatusCode.InternalServerError)
                    };
                    break;
            }
            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(Problem);
        }
    }
}
