
namespace Roamler.Application.DTO;

public record LocationInfo(double Latitude, double Longitude, string Address, double distance) : LocationWithAddress(Latitude, Longitude, Address);