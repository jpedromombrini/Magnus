using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class AppConfigurationMapper
{
    #region Request

    public static AppConfiguration MapToEntity(this CreateAppConfigurationRequest request)
    {
        return new AppConfiguration(request.CostCenterSale);
    }
    public static AppConfiguration MapToEntity(this UpdateAppConfigurationRequest request)
    {
        return new AppConfiguration(request.CostCenterSale);
    }
    public static IEnumerable<AppConfiguration> MapToEntity(
        this IEnumerable<CreateAppConfigurationRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    public static IEnumerable<AppConfiguration> MapToEntity(
        this IEnumerable<UpdateAppConfigurationRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }
    

    #endregion

    #region Response

    public static AppConfigurationResponse MapToResponse(this AppConfiguration entity)
    {
        return new AppConfigurationResponse(entity.Id, entity.CostCenterSale);
    }

    public static IEnumerable<AppConfigurationResponse> MapToResponse(this IEnumerable<AppConfiguration> entity)
    {
        return entity.Select(MapToResponse).ToList();
    }

    #endregion
}