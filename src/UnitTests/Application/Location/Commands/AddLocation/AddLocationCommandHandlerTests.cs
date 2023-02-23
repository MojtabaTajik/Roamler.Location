using FluentAssertions;
using NSubstitute;
using Roamler.Application.DTO;
using Roamler.Application.Location.Commands.AddLocation;
using Roamler.Application.Services;

namespace UnitTests.Application.Location.Commands.AddLocation;

public class AddLocationCommandHandlerTests
{
    private readonly ILocationService _locationService;
    private readonly AddLocationCommandHandler _handler;
    
    public AddLocationCommandHandlerTests()
    {
        _locationService = Substitute.For<ILocationService>();
        _handler = new AddLocationCommandHandler(_locationService);
    }

    [Fact]
    public async Task Add_single_location_should_return_true()
    {
        // Arrange
        var locationToAdd = new LocationWithAddress(15.1, 37.6, "Roamler HQ");
        var command = new AddLocationCommand(locationToAdd);

        _locationService.AddLocation(Arg.Any<LocationWithAddress>())
            .Returns(true);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().Be(true);
    }
}