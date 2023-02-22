using Roamler.Application.DTO;

namespace Roamler.Application.Services;

public interface IRedisService
{
    public Task<bool> AddLocation(LocationWithAddress loc);
    public Task<bool> AddLocationRange(List<LocationWithAddress> locs);
    public Task<List<LocationInfo>> GetNearLocations(Location sourceLoc, int maxDistance, int maxResult);
}