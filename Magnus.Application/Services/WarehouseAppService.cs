using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class WarehouseAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IWarehouseAppService
{
    public async Task AddWarehouseAsync(CreateWarehouseRequest request, CancellationToken cancellationToken)
    {
        var warehouseDb =
            await unitOfWork.Warehouses.GetByExpressionAsync(x => x.Name.ToLower() == request.Name.ToLower(),
                cancellationToken);
        if (warehouseDb != null)
            throw new ApplicationException("já existe um depósito com esse nome");
        await unitOfWork.Warehouses.AddAsync(mapper.Map<Warehouse>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWarehouseAsync(Guid id, UpdateWarehouseRequest request, CancellationToken cancellationToken)
    {
        var warehouseDb = await unitOfWork.Warehouses.GetByIdAsync(id, cancellationToken);
        if (warehouseDb == null)
            throw new EntityNotFoundException("depósito não encontrado");
        var user = await unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
            throw new EntityNotFoundException("usuário não encontrado");
        warehouseDb.SetName(request.Name);
        warehouseDb.SetUser(user);
        unitOfWork.Warehouses.Update(warehouseDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<WarehouseResponse>> GetWarehousesAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<WarehouseResponse>>(await unitOfWork.Warehouses.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<WarehouseStockResponse>> GetStockByUserAsync(Guid warehouseId,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(warehouseId, cancellationToken);
        if (user == null)
            throw new BusinessRuleException("Usuário não encontrado");

        var response = new List<WarehouseStockResponse>();
        var warehouses = await unitOfWork.Warehouses.GetAllByExpressionAsync(x =>
            user.UserType == UserType.Admin || x.UserId == user.Id, cancellationToken);
        var productsDb = await unitOfWork.Products.GetAllAsync(cancellationToken);
        var warehousePrincipal = new Warehouse("Principal", user);
        warehousePrincipal.SetCode(0);
        var enumerable = warehouses.ToList();
        enumerable.Add(warehousePrincipal);
        foreach (var warehouse in enumerable)
        {
            var productResponse = new List<ProductByStockResponse>();
            foreach (var product in productsDb)
            {
                var stock = await unitOfWork.ProductStocks.GetByExpressionAsync(x =>
                        x.ProductId == product.Id &&
                        x.WarehouseId == warehouse.Code,
                    cancellationToken);
                var amount = 0;
                if (stock != null)
                    amount = stock.Amount;

                productResponse.Add(new ProductByStockResponse(product.Id, product.Name, product.ApplyPriceRule,
                    product.Bars.MapToResponse(),
                    warehouse.Id, amount));
            }

            response.Add(new WarehouseStockResponse(warehouse.Code, warehouse.Id, warehouse.Name, productResponse));
        }

        return response;
    }

    public async Task<IEnumerable<WarehouseResponse>> ListWarehousesByFilterAsync(
        Expression<Func<Warehouse, bool>> predicate, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<WarehouseResponse>>(
            await unitOfWork.Warehouses.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<WarehouseResponse> GetWarehousesByFilterAsync(
        Expression<Func<Warehouse, bool>> predicate, CancellationToken cancellationToken)
    {
        return mapper.Map<WarehouseResponse>(
            await unitOfWork.Warehouses.GetByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<WarehouseResponse> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<WarehouseResponse>(await unitOfWork.Warehouses.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteWarehouseAsync(Guid id, CancellationToken cancellationToken)
    {
        var warehouseDb = await unitOfWork.Warehouses.GetByIdAsync(id, cancellationToken);
        if (warehouseDb == null)
            throw new EntityNotFoundException("depósito não encontrado");
        unitOfWork.Warehouses.Delete(warehouseDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}