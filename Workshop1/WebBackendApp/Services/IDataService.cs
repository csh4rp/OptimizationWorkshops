using WebBackendApp.Models;

namespace WebBackendApp.Services;

public interface IDataService
{
    Task SaveAsync(DataFrameDto dto, CancellationToken cancellationToken);
    
    Task<DataFrameSummaryDto> GetSummaryAsync(CancellationToken cancellationToken);
}
