using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class AppConfigurationService(IUnitOfWork unitOfWork) : IAppConfigurationService
{
    public async Task<AppConfiguration> GetAppConfigurationAsync(CancellationToken cancellationToken)
    {
        var configuration = await unitOfWork.AppConfigurations.GetAllAsync(cancellationToken);
        return configuration.FirstOrDefault() ?? throw new InvalidOperationException();
    }
}