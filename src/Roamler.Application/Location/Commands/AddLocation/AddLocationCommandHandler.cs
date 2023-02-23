using Roamler.Application.Abstraction.Command;
using Roamler.Application.Services;
using Roamler.Domain.Shared;

namespace Roamler.Application.Location.Commands.AddLocation;

public class AddLocationCommandHandler : ICommandHandler<AddLocationCommand, bool>
{
    private readonly ILocationService _locationService;

    public AddLocationCommandHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    public async Task<Result<bool>> Handle(AddLocationCommand request, CancellationToken cancellationToken)
    {
        var addResult = await _locationService.AddLocation(request.loc);
        
        return Result<bool>.Success(addResult);
    }
}