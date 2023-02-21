using Roamler.Application.Services;
using Roamler.Domain.Model;
using StackExchange.Redis;

namespace Roamler.Infrastructure.Services;

public class RedisService : IRedisService
{
    private readonly IDatabase _db;
    private readonly RedisKey _key;

    public RedisService(IConnectionMultiplexer connection)
    {
        _db = connection.GetDatabase();
        _key = new RedisKey("roamler:netherlands");
    }

    public async Task<bool> AddLocation(Location loc)
    {
        return await _db.GeoAddAsync(
            _key,
            loc.Longitude,
            loc.Latitude,
            new RedisValue(loc.Address));
    }

    public async Task<bool> AddLocationRange(List<Location> locs)
    {
        Parallel.ForEach(locs, async loc =>
        {
            await _db.GeoAddAsync(
                _key,
                loc.Longitude,
                loc.Latitude,
                new RedisValue(loc.Address));
        });

        return await Task.FromResult(true);
    }
}