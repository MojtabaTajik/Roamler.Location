using EasyCaching.Core;
using Roamler.Application.Abstraction.Query;
using Roamler.Application.DTO;
using Roamler.Application.Services;
using Roamler.Domain.Shared;

namespace Roamler.Application.Location.Queries.SearchNearLocations;

public class SearchNearLocationQueryHandler : IQueryHandler<SearchNearLocationsQuery, List<LocationInfo>>
{
    private readonly ILocationService _locationService;
    private readonly IEasyCachingProvider _cachingProvider;

    public SearchNearLocationQueryHandler(ILocationService locationService, IEasyCachingProvider cachingProvider)
    {
        _locationService = locationService;
        _cachingProvider = cachingProvider;
    }
    
    public async Task<Result<List<LocationInfo>>> Handle(SearchNearLocationsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = request.sourceLoc.ToString();
        
        var keyExists = await _cachingProvider.ExistsAsync(cacheKey, cancellationToken);
        if (keyExists)
        {
            var cachedLocations = await _cachingProvider.GetAsync<List<LocationInfo>>(cacheKey, cancellationToken);
            return Result<List<LocationInfo>>.Success(cachedLocations.Value, true);
        }

        var nearLocations =
            await _locationService.GetNearLocations(request.sourceLoc, request.maxDistance, request.maxResult);

        await _cachingProvider.SetAsync(cacheKey, nearLocations, TimeSpan.FromSeconds(30), cancellationToken);
        return Result <List<LocationInfo>>.Success(nearLocations);
    }
}