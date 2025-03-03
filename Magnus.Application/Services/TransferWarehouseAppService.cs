using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class TransferWarehouseAppService(
    IUnitOfWork unitOfWork,
    ITransferWarehouseService transferWarehouseService,
    IMapper mapper) : ITransferWarehouseAppService
{
    public async Task AddTransferWarehouseAsync(CreateTransferWarehouseRequest request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
            throw new EntityNotFoundException("Usuário não encontrado");
        var transferWarehouse = mapper.Map<TransferWarehouse>(request);
        transferWarehouse.SetCreatedAt();
        await unitOfWork.TransferWarehouses.AddAsync(transferWarehouse, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateTransferWarehouseAsync(Guid id, UpdateTransferWarehouseRequest request,
        CancellationToken cancellationToken)
    {
        var transferWarehouseDb = await unitOfWork.TransferWarehouses.GetByIdAsync(id, cancellationToken);
        if (transferWarehouseDb is null)
            throw new EntityNotFoundException(id);
        var items = mapper.Map<IEnumerable<TransferWarehouseItem>>(request.Items);
        transferWarehouseDb.SetUserId(request.UserId);
        transferWarehouseDb.SetUserName(request.UserName);
        transferWarehouseDb.SetWarehouseDestinyId(request.WarehouseDestinyId);
        transferWarehouseDb.SetWarehouseDestinyName(request.WarehouseDestinyName);
        transferWarehouseDb.SetWarehouseOriginId(request.WarehouseOriginId);
        transferWarehouseDb.SetWarehouseOriginName(request.WarehouseOriginName);
        transferWarehouseDb.ResetItems();
        foreach (var item in items)
        {
            transferWarehouseDb.AddItem(item);
        }

        unitOfWork.TransferWarehouses.Update(transferWarehouseDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTransferWarehouseItemStatusAsync(UpdateStatusTransferWarehouseItemRequest request,
        CancellationToken cancellationToken)
    {
        var transferItemDb = await unitOfWork.TransferWarehouses.GetItemByIdAsync(request.Id, cancellationToken);
        if (transferItemDb is null)
            throw new EntityNotFoundException("Não foi possível encontrar o item");
        var transferDb = await unitOfWork.TransferWarehouses.GetByIdAsync(transferItemDb.TransferWarehouse.Id, cancellationToken);
        if (transferDb is null)
            throw new EntityNotFoundException("Não foi possível encontrar a transferência");
        
        switch (transferItemDb.Status)
        {
            case TransferWarehouseItemStatus.Requested when
                request.Status == TransferWarehouseItemStatus.Transferred:
                await transferWarehouseService.ConfirmTransferWarehouse(transferItemDb, transferDb.Id,
                    transferDb.WarehouseOriginId, transferDb.WarehouseDestinyId, cancellationToken);
                break;
            case TransferWarehouseItemStatus.Transferred when
                request.Status == TransferWarehouseItemStatus.Refused:
                await transferWarehouseService.CancelTransferWarehouse(transferItemDb, transferDb.Id,
                    transferDb.WarehouseOriginId, transferDb.WarehouseDestinyId, cancellationToken);
                break;
            case TransferWarehouseItemStatus.Refused when
                request.Status == TransferWarehouseItemStatus.Transferred:
                await transferWarehouseService.ConfirmTransferWarehouse(transferItemDb, transferDb.Id,
                    transferDb.WarehouseOriginId, transferDb.WarehouseDestinyId, cancellationToken);
                break;
            case TransferWarehouseItemStatus.Canceled:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        transferItemDb.SetStatus(request.Status);
        unitOfWork.TransferWarehouses.UpdateStatusAsync(transferItemDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TransferWarehouseResponse>> GetTransferWarehousesAsync(
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<TransferWarehouseResponse>>(
            await unitOfWork.TransferWarehouses.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<TransferWarehouseResponse>> GetTransferWarehouseByFilterAsync(
        Expression<Func<TransferWarehouse, bool>> predicate, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<TransferWarehouseResponse>>(
            await unitOfWork.TransferWarehouses.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<IEnumerable<TransferWarehouseItemResponse>> GetTransferWarehouseItemsByFilterAsync(
        Expression<Func<TransferWarehouseItem, bool>> predicate, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<TransferWarehouseItemResponse>>(
            await unitOfWork.TransferWarehouses.GetItemsByStatusAsync(predicate, cancellationToken));
    }

    public async Task<TransferWarehouseResponse> GetTransferWarehouseByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return mapper.Map<TransferWarehouseResponse>(
            await unitOfWork.TransferWarehouses.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteTransferWarehouseAsync(Guid id, CancellationToken cancellationToken)
    {
        var transferWarehouseDb = await unitOfWork.TransferWarehouses.GetByIdAsync(id, cancellationToken);
        if (transferWarehouseDb is null)
            throw new EntityNotFoundException(id);
        unitOfWork.TransferWarehouses.Delete(transferWarehouseDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}