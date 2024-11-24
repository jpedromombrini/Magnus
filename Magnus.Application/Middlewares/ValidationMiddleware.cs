using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Magnus.Application.Middlewares;

public class ValidationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put || context.Request.Method == HttpMethods.Patch)
        {
            var requestType = context.Request.Path.Value?.Split('/').LastOrDefault();
            if (requestType != null)
            {
                try
                {
                    var validatorType = Type.GetType($"Magnus.Application.Dtos.Requests.{requestType}Validator");

                    if (validatorType != null)
                    {
                        var validator = (IValidator)Activator.CreateInstance(validatorType)!;
                        var modelType = validatorType.GetGenericArguments().FirstOrDefault();

                        if (modelType != null)
                        {
                            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                            var deserializedRequest = JsonSerializer.Deserialize(requestBody, modelType);

                            if (deserializedRequest != null)
                            {
                                var validationContext = new ValidationContext<object>(deserializedRequest);
                                var cancellationToken = context.RequestAborted;
                                var validationResult = await validator.ValidateAsync(validationContext, cancellationToken);

                                if (!validationResult.IsValid)
                                {
                                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                    await context.Response.WriteAsJsonAsync(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) }, cancellationToken: cancellationToken);
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Erro interno do servidor.");
                    return;
                }
            }
        }
        await _next(context);
    }
}