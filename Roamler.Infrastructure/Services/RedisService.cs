using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Roamler.Application.DTO;
using Roamler.Application.Services;
using StackExchange.Redis;

namespace Roamler.Infrastructure.Services;

public class RedisService : ILocationService
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

        var sw = new Stopwatch();
        sw.Start();

        foreach (var loc in locs)
        {
            try
            {
                _db.GeoAdd(
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

        sw.Stop();
        _logger.LogInformation(Consts.addRangeLocationDuration, locs.Count, sw.Elapsed);


        return await Task.FromResult(true);
    }

    public async Task<List<LocationInfo>> GetNearLocations(Location sourceLoc, int maxDistance, int maxResult)
    {
        var searchCircleInfo = new GeoSearchCircle(maxDistance, GeoUnit.Meters);
        var resultParams = GeoRadiusOptions.WithCoordinates | GeoRadiusOptions.WithDistance;

        var sw = new Stopwatch();
        sw.Start();
        var nearLocations = await _db.GeoSearchAsync(_key, 
            sourceLoc.Longitude, 
            sourceLoc.Latitude,
            searchCircleInfo,
            maxResult,
            true,
            Order.Ascending,
            resultParams);

        sw.Stop();
        _logger.LogInformation(Consts.searchLocationDuration, sourceLoc, maxDistance, sw.Elapsed, nearLocations.Length);
        
        return nearLocations
            .Select(s => new LocationInfo(s.Position.Value.Latitude,
                s.Position.Value.Longitude,
                s.Member,
                s.Distance.Value))
            .ToList();
    }
}