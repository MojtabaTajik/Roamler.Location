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

    [Fact]
    public async Task Should_return_success_on_add_location()
    {
        const string endpoint = "/Location/AddLocation";
        
        // Arrange
        var loc = new LocationWithAddress(37,15.3, "Roamler HQ");

        // Act
        var response = await _client.PutAsJsonAsync(endpoint, loc);

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task Should_return_success_on_valid_csv_file()
    {
        const string endpoint = "/Location/AddLocationsFromCsv";
        
        // Arrange
        var locs = new StringBuilder(10);
        locs.Append("Latitude,Longitude,Address");
        for (int i = 0; i <= 10; i++)
        {
            locs.AppendLine();
            locs.Append($"{20},{10},Roamler HQ");
        }

        var stringBytes = Encoding.UTF8.GetBytes(locs.ToString());
        var byteArrayContent = new ByteArrayContent(stringBytes);
        byteArrayContent.Headers.Remove("Content-Type");
        byteArrayContent.Headers.Add("Content-Type", "multipart/form-data");

        var content = new MultipartFormDataContent();
        content.Add(byteArrayContent, "csvFile", "Sample.csv");

        // Act
        var response = await _client.PostAsync(endpoint, content);

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task Should_return_fail_on_invalid_csv_file()
    {
        const string endpoint = "/Location/AddLocationsFromCsv";
        
        // Arrange
        var locs = new StringBuilder(10);
        locs.Append("Latitude,Longitude");
        for (int i = 0; i <= 10; i++)
        {
            locs.AppendLine();
            locs.Append($"{20},{10},Roamler HQ");
        }

        var stringBytes = Encoding.UTF8.GetBytes(locs.ToString());
        var byteArrayContent = new ByteArrayContent(stringBytes);
        byteArrayContent.Headers.Remove("Content-Type");
        byteArrayContent.Headers.Add("Content-Type", "multipart/form-data");

        var content = new MultipartFormDataContent();
        content.Add(byteArrayContent, "csvFile", "Sample.csv");

        // Act
        var response = await _client.PostAsync(endpoint, content);

        // Assert
        response.StatusCode.Should().NotBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Should_return_success_on_get_near_locations()
    {
        const string endpoint = "/Location/GetLocations?";
        
        // Arrange
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["Latitude"] = "37";
        query["Longitude"] = "15.3";
        query["maxDistance"] = "100";
        query["maxResults"] = "10";

        // Act
        var response = await _client.GetAsync(string.Concat(endpoint, query));

        // Assert
        response.EnsureSuccessStatusCode();
    }
}