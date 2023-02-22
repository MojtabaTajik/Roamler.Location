namespace Roamler.Application.DTO;

public record LocationWithAddress(double Latitude, double Longitude, string Address) : Location(Latitude, Longitude);