using ATM.CustomMiddlewares;

namespace ATM.Configuration.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ExceptionMiddleware>();
}
