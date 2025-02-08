using System.Globalization;
using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Magnus.Application.Services;

public class EstimateAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IEstimateAppService
{
    public async Task AddEstimateAsync(CreateEstimateRequest request, CancellationToken cancellationToken)
    {
        var estimate = mapper.Map<Estimate>(request);
        estimate.SetCreatedAt(DateTime.Now);
        await unitOfWork.Estimates.AddAsync(estimate, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateEstimateAsync(Guid id, UpdateEstimateRequest request, CancellationToken cancellationToken)
    {
        var estimateDb = await unitOfWork.Estimates.GetByIdAsync(id, cancellationToken);
        if (estimateDb == null)
            throw new EntityNotFoundException(id);
        
        estimateDb.SetDescription(request.Description);
        estimateDb.SetFreight(request.Freight);
        estimateDb.SetObservation(request.Observation);
        if (request.ClientId != null && request.ClientId != Guid.Empty)
        {
            estimateDb.SetClientId(request.ClientId);
            estimateDb.SetClientName(request.ClientName);
        }
        estimateDb.SetValue(request.Value);
        estimateDb.SetValidity(request.ValiditAt);
        estimateDb.SetUserId(request.UserId);
        
        foreach (var itemRequest in request.Items)
        {
            var existingItem = estimateDb.Items.SingleOrDefault(item => item.ProductId == itemRequest.ProductId);
            if (existingItem != null)
            {
                existingItem.SetProductName(itemRequest.ProductName); 
                existingItem.SetAmount(itemRequest.Amount); 
                existingItem.SetDiscount(itemRequest.Discount);
                existingItem.setValue(itemRequest.Value);
                existingItem.SetTotalValue(itemRequest.TotalValue);
            }
            else
            {
                var newItem = mapper.Map<EstimateItem>(itemRequest);
                estimateDb.AddItem(newItem);
            }
        }
        var itemsToRemove = estimateDb.Items.Where(item => request.Items.All(requestItem => requestItem.ProductId != item.ProductId)).ToList();
        unitOfWork.Estimates.DeleteItensRange(itemsToRemove);
        unitOfWork.Estimates.Update(estimateDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<EstimateResponse>> GetEstimatesAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<EstimateResponse>>(await unitOfWork.Estimates.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<EstimateResponse>> GetEstimatesByFilterAsync(
        Expression<Func<Estimate, bool>> predicate, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<EstimateResponse>>(
            await unitOfWork.Estimates.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<EstimateResponse> GetEstimateByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<EstimateResponse>(await unitOfWork.Estimates.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteEstimateAsync(Guid id, CancellationToken cancellationToken)
    {
        var estimateDb = await unitOfWork.Estimates.GetByIdAsync(id, cancellationToken);
        if (estimateDb == null)
            throw new EntityNotFoundException(id);
        unitOfWork.Estimates.Delete(estimateDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<byte[]> CreatePdf(Guid id, CancellationToken cancellationToken)
    {
        var estimateDb = await unitOfWork.Estimates.GetByIdAsync(id, cancellationToken);
        if (estimateDb == null)
            throw new EntityNotFoundException(id);
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.Content().Column(column =>
                {
                    column.Spacing(14);
                    column.Item().Text($"Daniel Magnus").FontSize(20).AlignCenter().Bold();
                    column.Item().Text($"Orçamento número {estimateDb.Code}").FontSize(16).Bold();
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"Enviado em {estimateDb.CreatedAt:dd/MM/yyyy}").FontSize(12);
                        row.RelativeItem().Text($"Válido até {estimateDb.ValiditAt:dd/MM/yyyy}").FontSize(12);
                    });
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"Vendedor: {estimateDb.User.Name}").FontSize(12);
                        if (!string.IsNullOrEmpty(estimateDb.ClientName))
                        {
                            row.RelativeItem().Text($"Cliente: {estimateDb.ClientName}").FontSize(12);
                        }
                    });
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });
                        table.Header(header =>
                        {
                            header.Cell().Text("Produto").Bold().FontSize(12).AlignLeft();
                            header.Cell().Text("Quantidade").Bold().FontSize(12).AlignRight();
                            header.Cell().Text("Valor Unitário").Bold().FontSize(12).AlignRight();
                            header.Cell().Text("Desconto").Bold().FontSize(12).AlignRight();
                            header.Cell().Text("Valor Total").Bold().FontSize(12).AlignRight();
                        });
                        foreach (var item in estimateDb.Items)
                        {
                            table.Cell().Text(item.ProductName).AlignLeft();
                            table.Cell().Text(item.Amount.ToString()).AlignRight();
                            table.Cell().Text($"{item.Value.ToString("c", new CultureInfo("pt-br"))}").AlignRight();
                            table.Cell().Text($"{item.Discount.ToString("c", new CultureInfo("pt-br"))}").AlignRight();
                            table.Cell().Text($"{item.TotalValue.ToString("c", new CultureInfo("pt-br"))}").AlignRight();
                        }
                    });
                    
                    var totalAmount = estimateDb.Items.Sum(item => item.TotalValue);
                    column.Item().Text($"Total: {totalAmount.ToString("c", new CultureInfo("pt-br"))}").FontSize(14).Bold();
                });
            });
        }).GeneratePdf();
    }
}