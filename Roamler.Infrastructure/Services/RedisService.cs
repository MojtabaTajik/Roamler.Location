using Microsoft.Extensions.Logging;
using Roamler.Application.DTO;
using Roamler.Application.Services;
using StackExchange.Redis;

namespace Roamler.Infrastructure.Services;

public class RedisService : IRedisService
{
    private readonly ILogger<RedisService> _logger;
    private readonly IDatabase _db;
    private readonly RedisKey _key;

    public RedisService(ILogger<RedisService> logger, IConnectionMultiplexer connection)
    {
        _logger = logger;
        _db = connection.GetDatabase();
        _key = new RedisKey("roamler:netherlands");
    }

    public async Task<bool> AddLocation(LocationWithAddress loc)
    {
        _logger.LogInformation(Consts.addLocationMessage);

        return await _db.GeoAddAsync(
            _key,
            loc.Longitude,
            loc.Latitude,
            new RedisValue(loc.Address));
    }

    public async Task<bool> AddLocationRange(List<LocationWithAddress> locs)
    {
        _logger.LogInformation(Consts.addRangeLocationMessage, locs.Count);
        
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
                _logger.LogError(ex.Message);
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