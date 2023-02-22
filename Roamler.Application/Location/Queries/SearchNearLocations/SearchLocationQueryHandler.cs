using Roamler.Application.Abstraction.Query;
using Roamler.Application.DTO;
using Roamler.Application.Services;
using Roamler.Domain.Shared;

namespace Roamler.Application.Location.Queries.SearchNearLocations;

public class SearchLocationQueryHandler : IQueryHandler<SearchNearLocationsQuery, List<LocationInfo>>
{
    private readonly ILocationService _locationService;

    public SearchLocationQueryHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    public async Task<Result<List<LocationInfo>>> Handle(SearchNearLocationsQuery request, CancellationToken cancellationToken)
    {
        var nearLocations =
            await _locationService.GetNearLocations(request.sourceLoc, request.maxDistance, request.maxResult);

        return Result < List<LocationInfo>>.Success(nearLocations);
    }
}