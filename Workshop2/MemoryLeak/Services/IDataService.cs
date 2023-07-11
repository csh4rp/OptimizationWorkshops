using MemoryLeak.Models;
using WebBackend.Models;

namespace MemoryLeak.Services;

public interface IDataService
{
    Task SaveAsync(DataFrameDto dto, CancellationToken cancellationToken);
    
    Task<DataFrameStdDevDto> GetSummaryAsync(DateTimeOffset tillTime, CancellationToken cancellationToken);
}
