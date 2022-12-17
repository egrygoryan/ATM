using System.Text.Json;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace ATM.CustomMiddlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next) => _next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static async Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = exception switch
        {
            InvalidOperationException => Status400BadRequest,
            ArgumentException => Status409Conflict,
            _ => Status500InternalServerError,
        };

        context.Response.ContentType = "application/json";
        var response = JsonSerializer.Serialize(new { exception.Message });
        await context.Response.WriteAsync(response);
    }
}
