using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class ProductGroupAppService(
    IUnitOfWork unitOfWork) : IProductGroupAppService
{
    public async Task AddProductGroupAsync(CreateProductGroupRequest request, CancellationToken cancellationToken)
    {
        var entity = request.MapToEntity();
        var productGroupExists =
            await unitOfWork.ProductGroups.GetByExpressionAsync(x => x.Name == entity.Name, cancellationToken);
        if (productGroupExists is not null)
            throw new BusinessRuleException("Já existe um Grupo com esse nome");
        await unitOfWork.ProductGroups.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductGroupAsync(Guid id, UpdateProductGroupRequest request,
        CancellationToken cancellationToken)
    {
        var entity = request.MapToEntity();
        var productGroup = await unitOfWork.ProductGroups.GetByIdAsync(id, cancellationToken);
        if (productGroup is null)
            throw new EntityNotFoundException("Nenhum grupo encontrado com esse id");
        productGroup.SetName(entity.Name);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductGroupResponse>> GetProductGroupsAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.ProductGroups.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<ProductGroupResponse>> GetProductGroupsByFilterAsync(
        Expression<Func<ProductGroup, bool>> predicate, CancellationToken cancellationToken)
    {
        return (await unitOfWork.ProductGroups.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<ProductGroupResponse> GetProductGroupByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var productGroup = await unitOfWork.ProductGroups.GetByIdAsync(id, cancellationToken);
        if (productGroup is null)
            throw new EntityNotFoundException("Nenhum grupo encontrado com esse id");
        return productGroup.MapToResponse();
    }

    public async Task DeleteProductGroupAsync(Guid id, CancellationToken cancellationToken)
    {
        var productGroup = await unitOfWork.ProductGroups.GetByIdAsync(id, cancellationToken);
        if (productGroup is null)
            throw new EntityNotFoundException("Nenhum grupo encontrado com esse id");
        unitOfWork.ProductGroups.Delete(productGroup);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}