using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class AppConfigurationService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IAppConfigurationService
{
    public async Task AddAppConfigurationAsync(CreateAppConfigurationRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.AppConfigurations.AddAsync(mapper.Map<AppConfiguration>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAppConfigurationAsync(Guid id, UpdateAppConfigurationRequest request, CancellationToken cancellationToken)
    {
        var appConfigurationDb = await unitOfWork.AppConfigurations.GetByIdAsync(id, cancellationToken);
        if (appConfigurationDb is null)
            throw new EntityNotFoundException(id);
        appConfigurationDb.SetAmountToDiscount(request.AmountToDiscount);
        unitOfWork.AppConfigurations.Update(appConfigurationDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<AppConfigurationResponse> GetAppConfigurationAsync(CancellationToken cancellationToken)
    {
        var appConfigurations = await unitOfWork.AppConfigurations.GetAllAsync(cancellationToken);
        return mapper.Map<AppConfigurationResponse>(appConfigurations.First());
    }
}