using MediatR;
using Microsoft.AspNetCore.Mvc;
using Roamler.API.Models.ApiResponse;
using Roamler.Application.CsvFile.Queries.ReadFileToLocation;
using Roamler.Application.DTO;
using Roamler.Application.Location.Commands.AddLocation;
using Roamler.Application.Location.Commands.AddLocationRange;
using Roamler.Application.Location.Queries.SearchNearLocations;

namespace Roamler.API.Controllers.Location;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly ISender _sender;

    public LocationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("AddLocation")]
    public async Task<IActionResult> AddLocation(LocationWithAddress loc)
    {
        var addLocationCommand = new AddLocationCommand(loc);
        await _sender.Send(addLocationCommand);

        return Ok();
    }

    [HttpPost("AddLocationsFromCsv")]
    public async Task<IActionResult> AddLocationsFromCsvFile(IFormFile csvFile)
    {
        var readCsvLocationsCommand = new ReadFileToLocationQuery(csvFile.OpenReadStream());
        var locations = await _sender.Send(readCsvLocationsCommand);
        if (locations.IsFailed)
        {
            return BadRequest();
        }

        var addLocationRangeCommand = new AddLocationRangeCommand(locations.Data);
        await _sender.Send(addLocationRangeCommand);

        return Ok();
    }

    [HttpGet("GetLocations")]
    public async Task<IActionResult> SearchNearLocations([FromQuery] Application.DTO.Location location, int maxDistance,
        int maxResults)
    {
        var searchNearLocations = new SearchNearLocationsQuery(location, maxDistance, maxResults);
        var nearLocations = await _sender.Send(searchNearLocations);

        return nearLocations.IsSuccess
            ? Ok(new CustomResponseList<LocationInfo>()
            {
                Status = ResponseStatus.Success,
                Data = nearLocations.Data,
                Count = nearLocations.Data.Count,
                Message = nearLocations.IsCached ? "Cached" : string.Empty
            })
            : NoContent();
    }
}