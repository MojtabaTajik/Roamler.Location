using Roamler.Application.Abstraction.Query;
using Roamler.Application.DTO;
using Roamler.Application.Services;
using Roamler.Domain.Shared;

namespace Roamler.Application.CsvFile.Queries.ReadFileToLocation;

public class ReadFileToLocationQueryHandler : IQueryHandler<ReadFileToLocationQuery, List<LocationWithAddress>>
{
    private readonly ICsvReaderService _csvReaderService;

    public ReadFileToLocationQueryHandler(ICsvReaderService csvReaderService)
    {
        _csvReaderService = csvReaderService;
    }

    public async Task<Result<List<LocationWithAddress>>> Handle(ReadFileToLocationQuery request,
        CancellationToken cancellationToken)
    {
        var locations = await _csvReaderService
            .ReadCsvToObjects<LocationWithAddress>(request.csvStream);

        return locations == null
            ? Result<List<LocationWithAddress>>.Failed()
            : Result<List<LocationWithAddress>>.Success(locations);
    }
}