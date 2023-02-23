using Microsoft.Extensions.DependencyInjection;
using Roamler.Application.Services;
using Roamler.Infrastructure.Services;
using StackExchange.Redis;

namespace Roamler.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ILocationService, RedisService>();
        services.AddTransient<ICsvReaderService, CsvReaderService>();
        services.AddEasyCaching(opt => opt.UseInMemory());
        services.AddRedisConnection();
    }

    private static void AddRedisConnection(this IServiceCollection services)
    {
        var redisConnection = Environment.GetEnvironmentVariable("RoamlerGeoRedisDB");
        
        var option = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            EndPoints = { redisConnection }
        };
        
        var multiplexer = ConnectionMultiplexer.Connect(option);
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
    }
}