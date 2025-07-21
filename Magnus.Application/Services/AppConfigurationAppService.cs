using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class AppConfigurationAppService(
    IUnitOfWork unitOfWork,
    ICostCenterService costCenterService) : IAppConfigurationAppService
{
    public async Task AddAppConfigurationAsync(CreateAppConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        await unitOfWork.AppConfigurations.AddAsync(request.MapToEntity(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAppConfigurationAsync(Guid id, UpdateAppConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        var appConfigurationDb = await unitOfWork.AppConfigurations.GetByIdAsync(id, cancellationToken);
        if (appConfigurationDb is null)
            throw new EntityNotFoundException("Configuração não encontrada");
        var costeCenter = await costCenterService.GetByIdAsync(request.CostCenterSale.Id, cancellationToken);
        if (costeCenter is null)
            throw new EntityNotFoundException("Nenhum centro de custo encontrado com o Id informado");
        appConfigurationDb.SetCostCenterSale(costeCenter);
        appConfigurationDb.SetAmountToDiscount(request.AmountToDiscount);
        appConfigurationDb.SetDaysValidityEstimate(request.DaysValidityEstimate);
        unitOfWork.AppConfigurations.Update(appConfigurationDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<AppConfigurationResponse> GetAppConfigurationAsync(CancellationToken cancellationToken)
    {
        var appConfigurations = await unitOfWork.AppConfigurations.GetAllAsync(cancellationToken);
        return appConfigurations.First().MapToResponse();
    }
}