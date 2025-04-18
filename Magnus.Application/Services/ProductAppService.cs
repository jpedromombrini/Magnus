using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class ProductAppService(
    IUnitOfWork unitOfWork,
    IProductService productService) : IProductAppService
{
    public async Task AddProductAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = request.MapToEntity();
        await productService.CreateProductAsync(product, cancellationToken);
        await unitOfWork.Products.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
    }

    public async Task UpdateProductAsync(Guid id, UpdateProductRequest request, CancellationToken cancellation)
    {
        var product = request.MapToEntity();
        var productDb = await productService.UpdateProductAsync(id, product, cancellation);
        unitOfWork.Products.Update(productDb);
        await unitOfWork.SaveChangesAsync(cancellation);
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Products.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(Expression<Func<Product, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await unitOfWork.Products.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<ProductResponse> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await productService.GetProductByIdAsync(id, cancellationToken);
        return product.MapToResponse();
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