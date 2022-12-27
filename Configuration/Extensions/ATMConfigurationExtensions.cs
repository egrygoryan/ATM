using ATM.Filters;
using ATM.Services;

namespace ATM.Configuration.Extensions;

public static class ATMConfigurationExtensions
{
  public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddSingleton<IATMService, ATMService>()
                .AddScoped<AuthorizeActionFilter>();
}
