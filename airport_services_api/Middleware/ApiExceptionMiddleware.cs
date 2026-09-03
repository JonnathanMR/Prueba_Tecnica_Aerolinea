using Microsoft.AspNetCore.Mvc;

namespace airport_services_api.Middleware;

public sealed class ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try { await next(context); }
        catch (Exception ex) when (ex is KeyNotFoundException or ArgumentException)
        {
            var status = ex switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status400BadRequest
            };
            logger.LogWarning(ex, "Solicitud rechazada en {Path}", context.Request.Path);
            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(new ProblemDetails { Status = status, Title = ex.Message });
        }
    }
}
