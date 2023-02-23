using EasyCaching.Core;
using FluentAssertions;
using NSubstitute;
using Roamler.Application.DTO;
using Roamler.Application.Location.Queries.SearchNearLocations;
using Roamler.Application.Services;

namespace UnitTests.Application.Location.Queries.SearchNearLocations;

public class SearchNeatLocationsQueryHandlerTests
{
    private readonly ILocationService _locationService;
    private readonly IEasyCachingProvider _cachingProvider;
    private readonly SearchNearLocationQueryHandler _handler;
    
    public SearchNeatLocationsQueryHandlerTests()
    {
        _cachingProvider = Substitute.For<IEasyCachingProvider>();
        _locationService = Substitute.For<ILocationService>();
        _handler = new SearchNearLocationQueryHandler(_locationService, _cachingProvider);
    }

    [Fact]
    public async Task Search_via_valid_cached_location_should_return_near_locs()
    {
        // Arrange
        var sourceLoc = new Roamler.Application.DTO.Location(15.1, 35.2);
        var query = new SearchNearLocationsQuery(sourceLoc, 1, 1);
        var expectedResult = new List<LocationInfo>
        {
            new(15.2, 35.6, "Roamler HQ", 3453)
        };
        var cacheKey = sourceLoc.ToString();

        _cachingProvider.ExistsAsync(cacheKey)
            .Returns(true);
        _cachingProvider.GetAsync<List<LocationInfo>>(cacheKey, CancellationToken.None)
            .Returns(Task.FromResult(new CacheValue<List<LocationInfo>>(expectedResult, true)));
        _locationService.GetNearLocations(sourceLoc, Arg.Any<int>(), Arg.Any<int>())
            .Returns(expectedResult);
        
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data.Should().Equal(expectedResult);
    }
    
    [Fact]
    public async Task Search_via_valid_none_cached_location_should_return_near_locs()
    {
        // Arrange
        var sourceLoc = new Roamler.Application.DTO.Location(15.1, 35.2);
        var query = new SearchNearLocationsQuery(sourceLoc, 1, 1);
        var expectedResult = new List<LocationInfo>
        {
            new(15.2, 35.6, "Roamler HQ", 3453)
        };
        var cacheKey = sourceLoc.ToString();
        
        _cachingProvider.ExistsAsync(cacheKey)
            .Returns(false);
        _locationService.GetNearLocations(sourceLoc, Arg.Any<int>(), Arg.Any<int>())
            .Returns(expectedResult);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().Be(true);
        result.Data.Count.Should().Be(1);
    }
}