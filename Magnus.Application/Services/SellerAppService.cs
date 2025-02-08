using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Services;

public class SellerAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ISellerAppService
{
    public async Task AddSellerAsync(CreateSellerRequest request, CancellationToken cancellationToken)
    {
        var sellerDb = await unitOfWork.Sellers.GetByExpressionAsync(
            x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (sellerDb is not null)
            throw new ApplicationException("Já existe um vendedor com esse nome");
        var user = new User(new Email(request.Email), request.Password, request.Name, DateTime.Now,
            DateTime.Now.AddYears(1), true, UserType.Seller);
        var seller = mapper.Map<Seller>(request);
        await unitOfWork.Users.AddAsync(user, cancellationToken);
        seller.SetUserId(user.Id);
        var warehouse = new Warehouse(request.Name, user);
        await unitOfWork.Sellers.AddAsync(seller, cancellationToken);
        await unitOfWork.Warehouses.AddAsync(warehouse, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSellerAsync(Guid id, UpdateSellerRequest request, CancellationToken cancellationToken)
    {
        var sellerDb = await unitOfWork.Sellers.GetByIdAsync(id, cancellationToken);
        if (sellerDb is null)
            throw new EntityNotFoundException(id);
        sellerDb.SetName(request.Name);
        sellerDb.SetEmail(new Email(request.Email));
        if(!string.IsNullOrEmpty(request.Document))
            sellerDb.SetDocument(new Document(request.Document));
        sellerDb.SetPhone(new Phone(request.Phone));
        unitOfWork.Sellers.Update(sellerDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SellerResponse>> GetSellersAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<SellerResponse>>(await unitOfWork.Sellers.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<SellerResponse>> GetSellersByFilterAsync(Expression<Func<Seller, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<SellerResponse>>(
            await unitOfWork.Sellers.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<SellerResponse> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<SellerResponse>(await unitOfWork.Sellers.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteSellerAsync(Guid id, CancellationToken cancellationToken)
    {
        var sellerDb = await unitOfWork.Sellers.GetByIdAsync(id, cancellationToken);
        if (sellerDb is null)
            throw new EntityNotFoundException(id);
        var userDb = await unitOfWork.Users.GetByIdAsync(sellerDb.UserId, cancellationToken);
        if (userDb is null)
            throw new EntityNotFoundException("Nenhum usuário encontrado");
        unitOfWork.Sellers.Delete(sellerDb);
        unitOfWork.Users.Delete(userDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}