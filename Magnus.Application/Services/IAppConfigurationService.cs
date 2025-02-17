using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services;

public interface IAppConfigurationService
{
    Task AddAppConfigurationAsync(CreateAppConfigurationRequest request, CancellationToken cancellationToken);
    Task UpdateAppConfigurationAsync(Guid id, UpdateAppConfigurationRequest request, CancellationToken cancellationToken);
    Task<AppConfigurationResponse> GetAppConfigurationAsync(CancellationToken cancellationToken);
}