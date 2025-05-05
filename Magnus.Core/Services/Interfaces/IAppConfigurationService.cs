using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IAppConfigurationService
{
    Task<AppConfiguration> GetAppConfigurationAsync(CancellationToken cancellationToken);
}