using WebBackend.Models;

namespace WebBackend.Services;

public interface IDataService
{
    Task SaveAsync(DataFrameDto dto, CancellationToken cancellationToken);
    
    Task<DataFrameSummaryDto> GetSummaryAsync(DateTimeOffset tillTime, CancellationToken cancellationToken);
}
