using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class ProductAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IProductAppService
{
    public async Task AddProductAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var productDb = await unitOfWork.Products.GetByExpressionAsync(
            x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (productDb is not null)
            throw new ApplicationException("Já existe um produto com esse nome");
        await unitOfWork.Products.AddAsync(mapper.Map<Product>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductAsync(Guid id, UpdateProductRequest request, CancellationToken cancellation)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellation);
        if (product is null)
            throw new EntityNotFoundException(id);
        var bars = mapper.Map<List<Bar>>(request.Bars);
        var priceRule = mapper.Map<PriceRule>(request.PriceRule);
        product.SetPrice(request.Price);
        product.SetName(request.Name);
        product.SetBars(bars);
        product.SetDiscount(request.Discount);
        product.SetLaboratoryId(request.LaboratoryId);
        product.SetPriceRule(priceRule);
        unitOfWork.Products.Update(product);
        await unitOfWork.SaveChangesAsync(cancellation);
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<ProductResponse>>(await unitOfWork.Products.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(Expression<Func<Product, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<ProductResponse>>(
            await unitOfWork.Products.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<ProductResponse> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            throw new EntityNotFoundException(id);
        return mapper.Map<ProductResponse>(product);
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            throw new EntityNotFoundException(id);
        unitOfWork.Products.Delete(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}