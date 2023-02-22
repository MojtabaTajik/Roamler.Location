using Microsoft.Extensions.DependencyInjection;
using Roamler.Application.Services;
using Roamler.Infrastructure.Services;
using StackExchange.Redis;

namespace Roamler.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ILocationService, RedisService>();
        services.AddTransient<ICsvReaderService, CsvReaderService>();
        services.AddRedisConnection();
        return services;
    }

    private static void AddRedisConnection(this IServiceCollection services)
    {
        var multiplexer = ConnectionMultiplexer.Connect("localhost:6380");
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
    }
}