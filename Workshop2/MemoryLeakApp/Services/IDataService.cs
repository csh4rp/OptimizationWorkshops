using MemoryLeakApp.Models;
using WebBackend.Models;

namespace MemoryLeakApp.Services;

public interface IDataService
{
    Task SaveAsync(DataFrameDto dto, CancellationToken cancellationToken);
    
    Task<DataFrameStdDevDto> GetSummaryAsync(DateTimeOffset tillTime, CancellationToken cancellationToken);
}
