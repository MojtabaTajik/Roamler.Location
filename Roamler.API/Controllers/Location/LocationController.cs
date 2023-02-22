using MediatR;
using Microsoft.AspNetCore.Mvc;
using Roamler.Application.DTO;
using Roamler.Application.Location.Commands.AddLocation;

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
}