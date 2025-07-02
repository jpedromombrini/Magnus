using System.Globalization;
using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Magnus.Application.Services;

public class EstimateAppService(
    IUnitOfWork unitOfWork,
    IEstimateService estimateService) : IEstimateAppService
{
    public async Task AddEstimateAsync(CreateEstimateRequest request, CancellationToken cancellationToken)
    {
        var estimate = request.MapToEntity();
        await estimateService.CreateEstimateAsync(estimate, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateSaleAsync(Guid id, CancellationToken cancellationToken)
    {
        await estimateService.CreateSaleAsync(id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateEstimateAsync(Guid id, UpdateEstimateRequest request, CancellationToken cancellationToken)
    {
        var estimate = request.MapToEntity();
        await estimateService.UpdateEstimateAsync(id, estimate, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<EstimateResponse>> GetEstimatesAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Estimates.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<EstimateResponse>> GetEstimatesByFilterAsync(
        Expression<Func<Estimate, bool>> predicate, CancellationToken cancellationToken)
    {
        return (await unitOfWork.Estimates.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<EstimateResponse> GetEstimateByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var estimateDb = await unitOfWork.Estimates.GetByIdAsync(id, cancellationToken);
        if (estimateDb is null)
            throw new EntityNotFoundException("Orçamento não encontrado");
        return estimateDb.MapToResponse();
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
        if (estimateDb.Receipts is not null)
            foreach (var receipt in estimateDb.Receipts)
            {
                var receiptDb = await unitOfWork.Receipts.GetByIdAsync(receipt.ReceiptId, cancellationToken);
                if (receiptDb is not null)
                    receipt.SetReceipt(receiptDb);
            }

        var freightName = "Frete";
        if (estimateDb.FreightId is not null)
        {
            var freightDb = await unitOfWork.Freights.GetByIdAsync((Guid)estimateDb.FreightId, cancellationToken);
            if (freightDb is not null)
                freightName = freightDb.Name;
        }


        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.Header().Column(header =>
                {
                    header.Item().Text("Estética Injetáveis").FontSize(20).Bold().AlignCenter();
                    header.Item().Text($"Orçamento número {estimateDb.Code}")
                        .FontSize(16).Bold().AlignCenter();
                    header.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);
                });

                static IContainer CellStyle(IContainer container)
                {
                    return container.PaddingVertical(5).PaddingHorizontal(2);
                }

                page.Content().Column(column =>
                {
                    column.Spacing(10);
                    if (!string.IsNullOrWhiteSpace(estimateDb.Description))
                        column.Item().Text(estimateDb.Description).Italic().FontColor(Colors.Grey.Darken2);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"Enviado em: {estimateDb.CreatedAt:dd/MM/yyyy}");
                        row.RelativeItem().Text($"Válido até: {estimateDb.ValiditAt:dd/MM/yyyy}");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"Vendedor: {estimateDb.User.Name}");
                        if (!string.IsNullOrEmpty(estimateDb.ClientName))
                            row.RelativeItem().Text($"Cliente: {estimateDb.ClientName}");
                    });

                    column.Item().LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten2);
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Produto").Bold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Quantidade").Bold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Valor Unitário").Bold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Valor Total").Bold();
                        });

                        foreach (var item in estimateDb.Items)
                        {
                            table.Cell().Element(CellStyle).Text(item.ProductName);
                            table.Cell().Element(CellStyle).AlignRight().Text(item.Amount.ToString());
                            table.Cell().Element(CellStyle).AlignRight()
                                .Text(item.Value.ToString("c", new CultureInfo("pt-br")));
                            table.Cell().Element(CellStyle).AlignRight()
                                .Text(item.TotalValue.ToString("c", new CultureInfo("pt-br")));
                        }
                    });

                    column.Item().LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten2);
                    var totalItems = estimateDb.Items.Sum(item => item.TotalValue);
                    var discount = estimateDb.FinantialDiscount;
                    var shipping = estimateDb.Freight;
                    var totalFinal = totalItems - discount + shipping;

                    column.Item().AlignRight().Column(totals =>
                    {
                        totals.Spacing(4);

                        // Helper para linha de texto
                        Func<IContainer, IContainer> LabelAndValue(string label, string value)
                        {
                            return container =>
                            {
                                container.Row(row =>
                                {
                                    row.ConstantItem(150).Text(label).Bold();
                                    row.RelativeItem().AlignRight().Text(value).Bold();
                                });
                                return container;
                            };
                        }

                        totals.Item().Element(LabelAndValue("Total dos itens:",
                            totalItems.ToString("c", new CultureInfo("pt-br"))));
                        totals.Item().Element(LabelAndValue("Desconto:",
                            $"-{discount.ToString("c", new CultureInfo("pt-br"))}"));
                        totals.Item()
                            .Element(
                                LabelAndValue($"{freightName} :", shipping.ToString("c", new CultureInfo("pt-br"))));

                        totals.Item().PaddingTop(10).Border(1).BorderColor(Colors.Grey.Medium)
                            .Background(Colors.Grey.Lighten3).Padding(10)
                            .Row(row =>
                            {
                                row.ConstantItem(150).Text("Total do orçamento:").Bold().FontSize(13);
                                row.RelativeItem().AlignRight().Text(totalFinal.ToString("c", new CultureInfo("pt-br")))
                                    .Bold().FontSize(13);
                            });
                    });
                    if (estimateDb.Receipts?.Any() == true)
                    {
                        column.Item().PaddingTop(15).Text("Formas de pagamento").Bold().FontSize(14);

                        foreach (var receipt in estimateDb.Receipts)
                        {
                            if (receipt.Installments?.Any() != true)
                                continue;

                            var totalReceipt = receipt.Installments.Sum(i => i.Value - i.Discount + i.Interest);
                            if (receipt.Installments.Count == 1)
                                column.Item().PaddingTop(5).AlignRight()
                                    .Text(
                                        $"{receipt.Receipt?.Name ?? "Recebimento"}: {totalReceipt.ToString("c", new CultureInfo("pt-br"))}")
                                    .FontSize(12);
                            else
                                column.Item().PaddingTop(5).AlignRight()
                                    .Text(
                                        $"{receipt.Receipt?.Name ?? "Recebimento"}: {totalReceipt.ToString("c", new CultureInfo("pt-br"))} em {receipt.Installments.Count} x")
                                    .FontSize(12);
                        }

                        var totalAllReceipts = estimateDb.Receipts
                            .SelectMany(r => r.Installments)
                            .Sum(i => i.Value - i.Discount + i.Interest);

                        column.Item().PaddingTop(10).AlignRight().BorderTop(1).BorderColor(Colors.Grey.Medium)
                            .PaddingTop(5)
                            .Text(
                                $"Total geral dos pagamentos: {totalAllReceipts.ToString("c", new CultureInfo("pt-br"))}")
                            .Bold().FontSize(13);
                    }
                    else
                    {
                        column.Item().PaddingTop(15).Text("Condição de pagamento a combinar").Bold().FontSize(14);
                    }

                    if (!string.IsNullOrWhiteSpace(estimateDb.Observation))
                        column.Item().PaddingTop(10).Text(text =>
                        {
                            text.Span("Observação: ").SemiBold();
                            text.Span(estimateDb.Observation);
                        });
                });
                page.Footer().AlignCenter().Column(footer =>
                {
                    footer.Item().Text(text =>
                    {
                        text.Span("Gerado em: ").SemiBold().FontSize(8);
                        text.Span($"{DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(8);
                    });
                });
            });
        }).GeneratePdf();
    }
}