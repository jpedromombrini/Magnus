using FluentValidation.AspNetCore;
using Magnus.Application.Dtos.Requests.Validators;
using Magnus.Application.Services;
using Magnus.Core.Repositories;
using Magnus.Core.Servicos;
using Magnus.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Magnus.Ioc;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        #region Services Application
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<ILaboratoryAppService, LaboratoryAppService>();
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IAuthAppService, AuthAppService>();
        services.AddScoped<IClientAppService, ClientAppAppService>();
        services.AddScoped<IDoctorAppService, DoctorAppService>();
        services.AddScoped<ISellerAppService, SellerAppService>();
        services.AddScoped<ISupplierAppService, SupplierAppService>();
        services.AddScoped<IReceiptAppService, ReceiptAppService>();
        services.AddScoped<ITransferWarehouseAppService, TransferWarehouseAppService>();
        services.AddScoped<ICostCenterAppService, CostCenterAppService>();
        services.AddScoped<ICostCenterGroupAppService, CostCenterGroupAppService>();
        services.AddScoped<ICostCenterSubGroupAppService, CostCenterSubGroupAppService>();
        services.AddScoped<IProductStockAppService, ProductStockAppService>();
        services.AddScoped<IInvoiceAppService, InvoiceAppService>();
        services.AddScoped<IPaymentAppService, PaymentAppService>();
        services.AddScoped<IInvoicePaymentAppService, InvoicePaymentAppService>();
        #endregion

        #region Services Core
        services.AddScoped<ITransferWarehouseService, TransferWarehouseService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        #endregion
        
        #region Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddFluentValidation(fv =>
            fv.RegisterValidatorsFromAssembly(typeof(CreateProductRequestValidator).Assembly));

        return services;
    }
}