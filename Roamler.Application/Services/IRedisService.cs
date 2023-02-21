using Roamler.Domain.Model;

namespace Roamler.Application.Services;

public interface IRedisService
{
    public bool AddLocation(Location loc);
    public bool AddLocationRange(List<Location> locs);
}