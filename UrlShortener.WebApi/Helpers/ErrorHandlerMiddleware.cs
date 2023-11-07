using System.Net;
using System.Text.Json;
using UrlShortener.Domain.Exceptions;

namespace UrlShortener.WebApi.Helpers;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (UrlNotFoundException ex)
        {
            await HandleExceptionAsync(httpContext,
                ex.Message,
                HttpStatusCode.NotFound,
                "The requested page was not found");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext,
                ex.Message,
                HttpStatusCode.InternalServerError,
                "Internal server error");
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, string exMsg, HttpStatusCode statusCode,
        string message)
    {
        _logger.LogError(exMsg);

        HttpResponse response = context.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(new { error = $"{message}" });

        await response.WriteAsJsonAsync(result);
    }
}