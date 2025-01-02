using System.Net;
using Magnus.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Magnus.Application.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;
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
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Ocorreu uma exceção não tratada.");
        context.Response.ContentType = "application/json";
        
        switch (exception)
        {
            case EntityNotFoundException:
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound; 
                var result = new
                {
                    message = exception.Message 
                };
                return context.Response.WriteAsJsonAsync(result);
            }
            case InvalidEmailException:
            default:
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                var result = new 
                {
                    message = exception.Message 
                };
                return context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}