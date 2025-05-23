using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;

namespace Magnus.Application.Mappers;

public static class TransferWarehouseMapper
{
    #region Requests

    public static TransferWarehouse MapToEntity(this CreateTransferWarehouseRequest request)
    {
        var transferWarehouse = new TransferWarehouse(request.UserId, request.UserName, request.WarehouseOriginId,
            request.WarehouseOriginName, request.WarehouseDestinyId, request.WarehouseDestinyName);
        foreach (var transferWarehouseItem in request.Items)
        {
            transferWarehouse.AddItem(transferWarehouseItem.MapToEntity());
        }

        return transferWarehouse;
    }

    public static TransferWarehouse MapToEntity(this UpdateTransferWarehouseRequest request)
    {
        var transferWarehouse = new TransferWarehouse(request.UserId, request.UserName, request.WarehouseOriginId,
            request.WarehouseOriginName, request.WarehouseDestinyId, request.WarehouseDestinyName);
        foreach (var transferWarehouseItem in request.Items)
        {
            transferWarehouse.AddItem(transferWarehouseItem.MapToEntity());
        }

        return transferWarehouse;
    }

    public static TransferWarehouseItem MapToEntity(this TransferWarehouseItemRequest request)
    {
        return new TransferWarehouseItem(request.ProductId, request.ProductInternalCode, request.ProductName,
            request.Amount, Guid.Empty, TransferWarehouseItemStatus.Requested, null);
    }

    #endregion

    #region Response

    public static TransferWarehouseResponse MapToResponse(this TransferWarehouse? entity)
    {
        var items = new List<TransferWarehouseItemResponse>(entity.Items.Count);
        items.AddRange(entity.Items.Select(item => new TransferWarehouseItemResponse(item.Id, item.ProductId,
            item.ProductInternalCode, item.ProductName, item.Amount, item.TransferWarehouseId, item.Status,
            entity.WarehouseOriginName, entity.WarehouseDestinyName)));

        var transferWarehouse = new TransferWarehouseResponse(entity.Id, entity.UserId, entity.CreatedAt,
            entity.UserName, entity.WarehouseOriginId, entity.WarehouseOriginName, entity.WarehouseDestinyId,
            entity.WarehouseDestinyName,
            items);
        return transferWarehouse;
    }

    public static TransferWarehouseItemResponse MapToResponse(this TransferWarehouseItem entity)
    {
        return new TransferWarehouseItemResponse(entity.Id, entity.ProductId,
            entity.ProductInternalCode, entity.ProductName, entity.Amount, entity.TransferWarehouseId, entity.Status,
            entity.TransferWarehouse.WarehouseOriginName, entity.TransferWarehouse.WarehouseDestinyName);
    }

    public static IEnumerable<TransferWarehouseItemResponse> MapToResponse(
        this IEnumerable<TransferWarehouseItem> entities)
    {
        return entities.Select(MapToResponse).ToList();
    }

    public static IEnumerable<TransferWarehouseResponse> MapToResponse(this IEnumerable<TransferWarehouse> entities)
    {
        return entities.Select(MapToResponse).ToList();
    }
    

    #endregion
}