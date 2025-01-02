using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Repositories;

namespace Magnus.Core.Servicos;

public class InvoiceService(
    IUnitOfWork unitOfWork) : IInvoiceService
{
    public async Task CreateInvoiceAsync(Invoice invoice, InvoicePayment invoicePayment,
        CancellationToken cancellationToken)
    {
        var productStock = invoice.Items
            .GroupBy(item => new { item.ProductId, item.Validity })
            .Select(group => new
            {
                group.Key.ProductId,
                group.Key.Validity,
                TotalAmount = group.Sum(i => i.Amount),
            })
            .ToList();
        List<ProductStock> listProductStock = [];
        foreach (var product in productStock)
        {
            var stock = await unitOfWork.ProductStocks
                .GetByExpressionAsync(x =>
                    x.ProductId == product.ProductId
                    && x.ValidityDate == product.Validity, cancellationToken);
            if (stock is null)
            {
                listProductStock.Add(
                    new ProductStock(product.ProductId, product.Validity, product.TotalAmount, 0, "Principal"));
            }
            else
            {
                stock.Amount += product.TotalAmount;
                unitOfWork.ProductStocks.Update(stock);
            }
        }

        await unitOfWork.ProductStocks.AddRangeAsync(listProductStock, cancellationToken);
        List<AuditProduct> audits = [];
        audits.AddRange(invoice.Items.Select(item => new AuditProduct(item.ProductId, DateTime.Now, invoice.Number,
            item.Amount, item.TotalValue, AuditProductType.In, invoice.SupplierId, invoice.Serie, 0, null, null,
            item.Validity)));
        await unitOfWork.Invoices.AddAsync(invoice, cancellationToken);
        await unitOfWork.AuditProducts.AddRangeAsync(audits, cancellationToken);
        invoicePayment.SetInvoice(invoice);
        foreach (var pay in invoicePayment.Installments)
        {
            invoicePayment.AddInstallment(new InvoicePaymentInstallment(invoicePayment,pay.DueDate,null,pay.Value, pay.Discount, pay.Interest, pay.Installment));
        }
        await unitOfWork.InvoicePayments.AddAsync(invoicePayment, cancellationToken);
    }

    public async Task DeleteInvoiceAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        var audits = await unitOfWork.AuditProducts.GetAllByExpressionAsync(x => x.Serie == invoice.Serie &&
            x.Document == invoice.Number &&
            x.SupplierId == invoice.SupplierId, cancellationToken);
        var payments = await unitOfWork.AccountsPayables.GetAllByExpressionAsync(
            x => x.InvoiceId == invoice.Id, cancellationToken);
        unitOfWork.AuditProducts.RemoveRange(audits);
        unitOfWork.AccountsPayables.RemoveRange(payments);
        unitOfWork.Invoices.Delete(invoice);
    }
}