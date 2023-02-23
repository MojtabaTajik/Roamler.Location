using Roamler.Application.DTO;

namespace Roamler.Application.Services;

public interface ILocationService
{
    public Task<bool> AddLocation(LocationWithAddress loc);
    public Task<bool> AddLocationRange(List<LocationWithAddress> locs);
    public Task<List<LocationInfo>> GetNearLocations(DTO.Location sourceLoc, int maxDistance, int maxResult);
}