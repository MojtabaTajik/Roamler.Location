using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Roamler.Application;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(r => r.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
    }
}