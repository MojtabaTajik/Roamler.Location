
namespace Roamler.Application.Services;

public interface ICsvReaderService
{
    public Task<List<T>> ReadCsvToObjects<T>(Stream fileContent);
}