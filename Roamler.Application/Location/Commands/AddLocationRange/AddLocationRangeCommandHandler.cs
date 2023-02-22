using Roamler.Application.Abstraction.Command;
using Roamler.Application.Services;
using Roamler.Domain.Shared;

namespace Roamler.Application.Location.Commands.AddLocationRange;

public class AddLocationRangeCommandHandler : ICommandHandler<AddLocationRangeCommand, bool>
{
    private readonly ILocationService _locationService;

    public AddLocationRangeCommandHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    public async Task<Result<bool>> Handle(AddLocationRangeCommand request, CancellationToken cancellationToken)
    {
        var addResult = await _locationService.AddLocationRange(request.locs);
        return addResult
            ? Result<bool>.Success() 
            : Result<bool>.Failed();
    }
}