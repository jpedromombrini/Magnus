using Magnus.Application.Services;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Repositories;
using Magnus.Core.Services;
using Magnus.Core.Services.Interfaces;
using Magnus.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using AppConfigurationService = Magnus.Core.Services.AppConfigurationService;

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
        services.AddScoped<IClientAppService, ClientAppService>();
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
        services.AddScoped<IAppConfigurationAppService, AppConfigurationAppService>();
        services.AddScoped<ISaleReceiptAppService, SaleReceiptAppService>();
        services.AddScoped<IAuditProductAppService, AuditProductAppService>();
        services.AddScoped<IAccountReceivableAppService, AccountReceivableAppService>();
        services.AddScoped<IStatisticsAppService, StatisticsAppService>();
        services.AddScoped<IFreightAppService, FreightAppService>();
        services.AddScoped<IStockMovementAppService, StockMovementAppService>();
        services.AddScoped<IAccountPayableAppService, AccountPayableAppService>();
        services.AddScoped<IReportAppService, ReportAppService>();
        services.AddScoped<ICampaignAppService, CampaignAppService>();
        services.AddScoped<IProductGroupAppService, ProductGroupAppService>();

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
        services.AddScoped<IAppConfigurationService, AppConfigurationService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ISellerService, SellerService>();
        services.AddScoped<IEstimateService, EstimateService>();
        services.AddScoped<IAccountPayableService, AccountPayableService>();
        services.AddScoped<IStockMovementService, StockMovementService>();
        services.AddScoped<ICampaignService, CampaignService>();

        #endregion

        #region Repositories

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}