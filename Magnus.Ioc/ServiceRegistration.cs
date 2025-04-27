using FluentValidation;
using FluentValidation.AspNetCore;
using Magnus.Application.Dtos.Requests.Validators;
using Magnus.Application.Services;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Repositories;
using Magnus.Core.Services;
using Magnus.Core.Services.Interfaces;
using Magnus.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Magnus.Ioc;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
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
        services.AddScoped<IEstimateAppService, EstimateAppService>();
        services.AddScoped<ISaleAppService, SaleAppService>();
        services.AddScoped<IWarehouseAppService, WarehouseAppService>();
        services.AddScoped<IAppConfigurationService, AppConfigurationService>();
        services.AddScoped<ISaleReceiptAppService, SaleReceiptAppService>();
        services.AddScoped<IAuditProductAppService, AuditProductAppService>();
        services.AddScoped<IAccountReceivableAppService, AccountReceivableAppService>();
        #endregion

        #region Services Core
        services.AddScoped<IAuditProductService, AuditProductService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IProductStockService, ProductStockService>();
        services.AddScoped<ISaleService, SaleService>();
        services.AddScoped<ISaleReceiptService, SaleReceiptService>();
        services.AddScoped<ITransferWarehouseService, TransferWarehouseService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IReceiptService, ReceiptService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductPriceTableService, ProductPriceTableService>();
        services.AddScoped<IBarService, BarService>();
        services.AddScoped<IAccountsReceivableService, AccountsReceivableService>();
        services.AddScoped<ICostCenterService, CostCenterService>();
        #endregion
        
        #region Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();
    }
}