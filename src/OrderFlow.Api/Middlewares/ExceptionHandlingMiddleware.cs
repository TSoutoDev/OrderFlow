using FluentValidation;
using System.Net;
using System.Text.Json;

namespace OrderFlow.Api.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware( RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            await HandleValidationExceptionAsync(context, exception);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Ocorreu um erro não tratado durante a requisição.");

            await HandleUnexpectedExceptionAsync(context);
        }
    }

    private static async Task HandleValidationExceptionAsync( HttpContext context, ValidationException exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        context.Response.ContentType ="application/json";

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message = "Erro de validação.",
            errors = exception.Errors.Select(error => new
            {
                property = error.PropertyName,
                message = error.ErrorMessage
            })
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static async Task HandleUnexpectedExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message = "Ocorreu um erro interno no servidor."
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}