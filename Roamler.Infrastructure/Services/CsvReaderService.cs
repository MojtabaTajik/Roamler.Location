using System.Globalization;
using System.Text;
using CsvHelper;
using Roamler.Application.Services;

namespace Roamler.Infrastructure.Services;

public class CsvReaderService : ICsvReaderService
{
    public async Task<List<T>> ReadCsvToObjects<T>(Stream fileContent)
    {
        using var reader = new StreamReader(fileContent, Encoding.UTF8);
        
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return await csv
            .GetRecordsAsync<T>()
            .ToListAsync();
    }
}