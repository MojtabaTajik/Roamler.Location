using Roamler.Application.Abstraction.Query;
using Roamler.Application.DTO;

namespace Roamler.Application.Location.Queries.SearchNearLocations;

public record SearchNearLocationsQuery(DTO.Location sourceLoc, int maxDistance, int maxResult) : IQuery<List<LocationInfo>>;