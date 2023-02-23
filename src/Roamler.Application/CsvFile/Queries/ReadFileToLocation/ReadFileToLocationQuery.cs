using Roamler.Application.Abstraction.Query;
using Roamler.Application.DTO;

namespace Roamler.Application.CsvFile.Queries.ReadFileToLocation;

public record ReadFileToLocationQuery(Stream csvStream) : IQuery<List<LocationWithAddress>>;