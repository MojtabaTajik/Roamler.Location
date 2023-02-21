namespace Roamler.Domain.Model;

public record Location
{
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
}