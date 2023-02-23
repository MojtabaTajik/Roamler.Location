using Roamler.Application.Abstraction.Command;
using Roamler.Application.DTO;

namespace Roamler.Application.Location.Commands.AddLocationRange;

public record AddLocationRangeCommand(List<LocationWithAddress> locs) : ICommand<bool>;