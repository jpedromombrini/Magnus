using System.Net;
using System.Text.Json;
using Magnus.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Magnus.Application.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;
    private readonly RequestDelegate _next = next;

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

        object result;

        if (exception is EntityNotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            result = new { message = exception.Message };
        }
        else if (exception is AuthenticationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            result = new { message = "Autenticação falhou." };
        }
        else if (exception is BusinessRuleException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            result = new { message = exception.Message };
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            result = new { message = "Erro interno no servidor." };
        }

        var json = JsonSerializer.Serialize(result);
        return context.Response.WriteAsync(json);
    }
}