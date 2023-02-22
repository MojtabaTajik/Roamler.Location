using System.Text;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Roamler.Application.CsvFile.Queries.ReadFileToLocation;
using Roamler.Application.DTO;
using Roamler.Infrastructure.Exceptions;
using Roamler.Infrastructure.Services;

namespace UnitTests.Application.CsvFile.Queries.ReadFileToLocation;

public class ReadFileToLocationQueryHandlerTests
{
    private readonly ReadFileToLocationQueryHandler _handler;

    public ReadFileToLocationQueryHandlerTests()
    {
        var logger = Substitute.For<ILogger<CsvReaderService>>();
        var csvReaderService = new CsvReaderService(logger);
        _handler = new ReadFileToLocationQueryHandler(csvReaderService);
    }

    [Fact]
    public async Task read_normal_file_should_return_locations()
    {
        // Arrange
        var csvString = $"Address,Latitude,Longitude{Environment.NewLine}Roamler HQ,1.1,2.2";
        var csvStream = new MemoryStream(Encoding.UTF8.GetBytes(csvString)); 
        var query = new ReadFileToLocationQuery(csvStream);

        var expectedLocation = new List<LocationWithAddress>
        {
            new(1.1, 2.2, "Roamler HQ")
        };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().Be(true);
        result.Data.Should().NotBeEmpty();
        result.Data.Should().Equal(expectedLocation);
    }
}