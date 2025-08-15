using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class SellerService(IUnitOfWork unitOfWork) : ISellerService
{
    public async Task AddSellerAsync(Seller seller, string password, CancellationToken cancellationToken)
    {
        var sellerDb = await unitOfWork.Sellers.GetByExpressionAsync(
            x => x.Name.ToLower() == seller.Name.ToLower(), cancellationToken);
        if (sellerDb is not null)
            throw new BusinessRuleException("Já existe um vendedor com esse nome");
        var userDb = await unitOfWork.Users.GetUserByEmailAsync(seller.Email.Address, cancellationToken);
        if (userDb is not null)
            throw new BusinessRuleException("Já existe um usuário com esse e-mail");
        var user = new User(seller.Email, password, seller.Name, DateTime.Now,
            DateTime.Now.AddYears(1), true, UserType.Seller);
        await unitOfWork.Users.AddAsync(user, cancellationToken);
        seller.SetUserId(user.Id);
        var warehouse = new Warehouse(seller.Name, user);
        await unitOfWork.Sellers.AddAsync(seller, cancellationToken);
        await unitOfWork.Warehouses.AddAsync(warehouse, cancellationToken);
    }

    public async Task UpdateSellerAsync(Guid id, Seller seller, CancellationToken cancellationToken)
    {
        var sellerDb = await unitOfWork.Sellers.GetByIdAsync(id, cancellationToken);
        if (sellerDb is null)
            throw new EntityNotFoundException(id);
        sellerDb.SetName(seller.Name);
        sellerDb.SetEmail(seller.Email);
        if (seller.Document is not null)
            sellerDb.SetDocument(seller.Document);
        sellerDb.SetPhone(seller.Phone);
        unitOfWork.Sellers.Update(sellerDb);
    }
}