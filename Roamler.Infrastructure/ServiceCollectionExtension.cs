using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Roamler.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddRedis();
        return services;
    }

    private static IServiceCollection AddRedis(this IServiceCollection services)
    {
        var multiplexer = ConnectionMultiplexer.Connect("localhost");
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);

        return services;
    }
}