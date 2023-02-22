using MediatR;
using Microsoft.AspNetCore.Mvc;
using Roamler.Application.CsvFile.Queries.ReadFileToLocation;
using Roamler.Application.DTO;
using Roamler.Application.Location.Commands.AddLocation;
using Roamler.Application.Location.Commands.AddLocationRange;

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
        var addResult = await _sender.Send(addLocationCommand);
        
        return Ok(addResult);
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
        var addResult = await _sender.Send(addLocationRangeCommand);
        
        return addResult.IsSuccess
            ? Ok() 
            : BadRequest();
    }
}