﻿using ATM.Services;

namespace ATM.Configuration.Extensions;

public static class ATMConfigurationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
          services.AddSingleton<IATMService, ATMService>()
                  .Decorate<IATMService, ATMEventService>()
                  .AddSingleton<IATMEventBroker, ATMEventBroker>()
                  .AddSingleton<MinimalATM>();
}