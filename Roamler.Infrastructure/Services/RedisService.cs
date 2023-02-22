using Roamler.Application.DTO;
using Roamler.Application.Services;
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

    public async Task<bool> AddLocation(LocationWithAddress loc)
    {
        return await _db.GeoAddAsync(
            _key,
            loc.Longitude,
            loc.Latitude,
            new RedisValue(loc.Address));
    }

    public async Task<bool> AddLocationRange(List<LocationWithAddress> locs)
    {
        foreach (var loc in locs)
        {
            try
            {
                await _db.GeoAddAsync(
                    _key,
                    loc.Longitude,
                    loc.Latitude,
                    new RedisValue(loc.Address));
            }
            catch (Exception ex)
            {
                
            }
        }

        return await Task.FromResult(true);
    }

    public async Task<List<LocationInfo>> GetNearLocations(Location sourceLoc, int maxDistance, int maxResult)
    {
        var searchCircleInfo = new GeoSearchCircle(maxDistance, GeoUnit.Meters);
        var resultParams = GeoRadiusOptions.WithCoordinates | GeoRadiusOptions.WithDistance;
        
        var nearLocations = await _db.GeoSearchAsync(_key, 
            sourceLoc.Longitude, 
            sourceLoc.Latitude,
            searchCircleInfo,
            maxResult,
            true,
            Order.Ascending,
            resultParams);

        return nearLocations
            .Select(s => new LocationInfo(s.Position.Value.Latitude,
                s.Position.Value.Longitude,
                s.Member,
                s.Distance.Value))
            .ToList();
    }
}