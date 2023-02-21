using Roamler.Domain.Model;

namespace Roamler.Application.Services;

public interface IRedisService
{
    public Task<bool> AddLocation(Location loc);
    public Task<bool> AddLocationRange(List<Location> locs);
}