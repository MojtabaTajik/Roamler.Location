namespace Roamler.API.Configuration;

public static class ApplicationBuilderExtension
{
    public static void AddSwagger(this IApplicationBuilder app, bool isDevelopment)
    {
        if (isDevelopment == true)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}