using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Web;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Roamler.Application.DTO;

namespace IntegrationTests;

public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient? _client;
    
    public IntegrationTest(WebApplicationFactory<Program> factory)
    {
        Environment.SetEnvironmentVariable("RoamlerGeoRedisDB", "localhost:6381");
        
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("/health")]
    public async Task Should_return_success_for_static_urls(string url)
    {
        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}