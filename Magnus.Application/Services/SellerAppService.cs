using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class SellerAppService(
    IUnitOfWork unitOfWork,
    ISellerService sellerService) : ISellerAppService
{
    public async Task AddSellerAsync(CreateSellerRequest request, CancellationToken cancellationToken)
    {
        await sellerService.AddSellerAsync(request.MapToEntity(), request.Password, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSellerAsync(Guid id, UpdateSellerRequest request, CancellationToken cancellationToken)
    {
        await sellerService.UpdateSellerAsync(id, request.MapToEntity(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SellerResponse>> GetSellersAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Sellers.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<SellerResponse>> GetSellersByFilterAsync(Expression<Func<Seller, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await unitOfWork.Sellers.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<SellerResponse> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var seller = await unitOfWork.Sellers.GetByIdAsync(id, cancellationToken);
        if (seller is null)
            throw new EntityNotFoundException(id);
        return (seller).MapToResponse();
    }

    public async Task DeleteSellerAsync(Guid id, CancellationToken cancellationToken)
    {
        var sellerDb = await unitOfWork.Sellers.GetByIdAsync(id, cancellationToken);
        if (sellerDb is null)
            throw new EntityNotFoundException(id);
        if (sellerDb.UserId is not null)
        {
            Guid userId = (Guid)sellerDb.UserId;
            var userDb = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (userDb is null)
                throw new EntityNotFoundException("Nenhum usuário encontrado");
            unitOfWork.Users.Delete(userDb);
        }

        unitOfWork.Sellers.Delete(sellerDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}