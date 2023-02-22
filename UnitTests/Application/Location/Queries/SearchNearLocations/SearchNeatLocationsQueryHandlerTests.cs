using FluentAssertions;
using NSubstitute;
using Roamler.Application.DTO;
using Roamler.Application.Location.Queries.SearchNearLocations;
using Roamler.Application.Services;

namespace UnitTests.Application.Location.Queries.SearchNearLocations;

public class SearchNeatLocationsQueryHandlerTests
{
    private readonly ILocationService _locationService;
    private readonly SearchNearLocationQueryHandler _handler;
    
    public SearchNeatLocationsQueryHandlerTests()
    {
        _locationService = Substitute.For<ILocationService>();
        _handler = new SearchNearLocationQueryHandler(_locationService);
    }

    [Fact]
    public async Task Search_via_valid_location_should_return_near_locs()
    {
        // Arrange
        var sourceLoc = new Roamler.Application.DTO.Location(15.1, 35.2);
        var query = new SearchNearLocationsQuery(sourceLoc, 1, 1);
        var expectedResult = new List<LocationInfo>
        {
            new(15.2, 35.6, "Roamler HQ", 3453)
        };
        
        _locationService.GetNearLocations(sourceLoc, Arg.Any<int>(), Arg.Any<int>())
            .Returns(expectedResult);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().Be(true);
        result.Data.Count.Should().Be(1);
    }
}