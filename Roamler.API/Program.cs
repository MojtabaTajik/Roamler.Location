using Roamler.API.Configuration;
using Roamler.Application;
using Roamler.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.AddSwagger(app.Environment.IsDevelopment());
app.MapControllers();
app.UseHealthChecks("/health");

await app.RunAsync(app.Lifetime.ApplicationStopped);