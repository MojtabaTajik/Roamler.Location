using FluentAssertions;
using NSubstitute;
using Roamler.Application.DTO;
using Roamler.Application.Location.Commands.AddLocation;
using Roamler.Application.Location.Commands.AddLocationRange;
using Roamler.Application.Services;

namespace UnitTests.Application.Location.Commands.AddLocationRange;

public class AddLocationRangeCommandHandlerTests
{
    private readonly ILocationService _locationService;
    private readonly AddLocationRangeCommandHandler _handler;
    
    public AddLocationRangeCommandHandlerTests()
    {
        _locationService = Substitute.For<ILocationService>();
        _handler = new AddLocationRangeCommandHandler(_locationService);
    }

    [Fact]
    public async Task Add_location_range_should_return_true()
    {
        // Arrange
        var locationsToAdd = new List<LocationWithAddress>
        {
            new(15.1, 37.6, "Roamler HQ"),
            new(15.1, 37.6, "Roamler Tech Building")
        };
        var command = new AddLocationRangeCommand(locationsToAdd);

        _locationService.AddLocationRange(Arg.Any<List<LocationWithAddress>>())
            .Returns(true);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().Be(true);
    }
}