using Roamler.API.Configuration;
using Roamler.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.AddSwagger(app.Environment.IsDevelopment());
app.MapControllers();
app.UseHealthChecks("/health");

app.Run();