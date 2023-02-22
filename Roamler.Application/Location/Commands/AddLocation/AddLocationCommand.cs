using Roamler.Application.Abstraction.Command;

namespace Roamler.Application.Location.Commands.AddLocation;

public record AddLocationCommand(DTO.LocationWithAddress loc) : ICommand<bool>;