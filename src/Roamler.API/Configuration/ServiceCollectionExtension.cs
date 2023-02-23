using System.Reflection;

namespace Roamler.API.Configuration;

public static class ServiceCollectionExtension
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSwaggerDocumentation();
        services.AddHealthChecks();
    }
    
    private static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(Consts.Swagger.ApiVersion, new()
            {
                Version = Consts.Swagger.ApiVersion,
                Title = Consts.Swagger.Title,
                Description = Consts.Swagger.Description,
                TermsOfService = new(Consts.Swagger.TermsOfService),
                Contact = new()
                {
                    Name = Consts.Swagger.ContactTitle,
                    Email = Consts.Swagger.ContactEmailAddress
                },
            });
    
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}