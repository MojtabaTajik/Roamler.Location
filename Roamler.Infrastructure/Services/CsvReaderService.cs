using System.Globalization;
using System.Text;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Roamler.Application.Services;
using Roamler.Infrastructure.Exceptions;

namespace Roamler.Infrastructure.Services;

public class CsvReaderService : ICsvReaderService
{
    private readonly ILogger<CsvReaderService> _logger;

    public CsvReaderService(ILogger<CsvReaderService> logger)
    {
        _logger = logger;
    }

    public async Task<List<T>> ReadCsvToObjects<T>(Stream fileContent)
    {
        try
        {
            using var reader = new StreamReader(fileContent, Encoding.UTF8);

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return await csv
                .GetRecordsAsync<T>()
                .ToListAsync();
        }
        catch (CsvHelperException)
        {
            throw new InvalidCsvFileException();
        }
    }
}