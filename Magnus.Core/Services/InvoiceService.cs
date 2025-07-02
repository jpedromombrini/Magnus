using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class InvoiceService(
    IUnitOfWork unitOfWork) : IInvoiceService
{
    public async Task CreateInvoiceAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        var invoiceDb =
            await unitOfWork.Invoices.GetByExpressionAsync(x => x.SupplierId == invoice.SupplierId
                                                                && x.Number == invoice.Number
                                                                && x.Serie == invoice.Serie, cancellationToken);
        if (invoiceDb is not null)
            throw new ApplicationException("Já existe uma NF com esses dados");
        var supplier = await unitOfWork.Suppliers.GetByIdAsync(invoice.SupplierId, cancellationToken);
        if (supplier is null)
            throw new EntityNotFoundException(invoice.SupplierId);
        var laboratory = await unitOfWork.Laboratories.GetByIdAsync(invoice.LaboratoryId, cancellationToken);
        if (laboratory is null)
            throw new EntityNotFoundException(invoice.LaboratoryId);
        invoice.SetSupplierName(supplier.Name);
        var totalItemsValue = invoice.Items
            .Where(x => x.Bonus == false)
            .Sum(x => x.TotalValue);
        ValidateFinantial(invoice);
        ValidateItems(invoice);
        var productStock = invoice.Items
            .GroupBy(item => new { item.ProductId })
            .Select(group => new
            {
                group.Key.ProductId,
                TotalAmount = group.Sum(i => i.Amount)
            })
            .ToList();
        List<ProductStock> listProductStock = [];
        foreach (var product in productStock)
        {
            var stock = await unitOfWork.ProductStocks
                .GetByExpressionAsync(x =>
                        x.ProductId == product.ProductId,
                    cancellationToken);
            if (stock is null)
            {
                listProductStock.Add(
                    new ProductStock(product.ProductId, product.TotalAmount, 0, "Principal"));
            }
            else
            {
                stock.IncreaseAmount(product.TotalAmount);
                unitOfWork.ProductStocks.Update(stock);
            }
        }

        await unitOfWork.ProductStocks.AddRangeAsync(listProductStock, cancellationToken);
        List<AuditProduct> audits = [];
        audits.AddRange(invoice.Items.Select(item => new AuditProduct(item.ProductId, DateTime.Now, invoice.Number,
            item.Amount, item.TotalValue, AuditProductType.In, invoice.SupplierId, invoice.Serie, 0, null, null,
            null)));
        await unitOfWork.Invoices.AddAsync(invoice, cancellationToken);
        await unitOfWork.AuditProducts.AddRangeAsync(audits, cancellationToken);
        await GenerateAccountsPayableAsync(invoice, cancellationToken);
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

    private static void ValidateFinantial(Invoice invoice)
    {
        if (invoice.UpdateFinantial)
        {
            var totalInstallments = invoice.InvoicePayments
                .SelectMany(x => x.Installments)
                .Sum(installment => installment.Value);
            if (invoice.GetRealValue() - totalInstallments != 0)
                throw new BusinessRuleException("Total das parcelas diverge do total do pedido");
            foreach (var invoicePayment in invoice.InvoicePayments)
                invoicePayment.SetSupplierId(invoice.SupplierId);
        }
    }

    private static void ValidateItems(Invoice invoice)
    {
        var totalItems = invoice.Items.Where(x => !x.Bonus).Sum(x => x.TotalValue);
        if (totalItems != invoice.Value)
            throw new BusinessRuleException("Total do total do pedido difere do total dos itens");
    }

    private async Task GenerateAccountsPayableAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        if (invoice is { UpdateFinantial: true, InvoicePayments: not null, CostCenterId: not null })
        {
            var costCenter = await unitOfWork.CostCenters.GetByIdAsync((Guid)invoice.CostCenterId, cancellationToken);
            if (costCenter is null)
                throw new BusinessRuleException("Informe um centro de custo");
            foreach (var invoicePayment in invoice.InvoicePayments)
            {
                var payment = await unitOfWork.Payments.GetByIdAsync(invoicePayment.PaymentId, cancellationToken);
                if (payment is null)
                    throw new BusinessRuleException("Pagamento não encontrado");
                foreach (var installment in invoicePayment.Installments)
                {
                    var accountsPayable = new AccountsPayable(invoice.Number, invoice.SupplierId, DateTime.Now,
                        installment.DueDate, installment.PaymentDate, installment.Value, 0m,
                        installment.Discount, installment.Interest, costCenter.Id, installment.Installment, invoice.Id,
                        null, invoice.LaboratoryId, payment.Id);
                    await unitOfWork.AccountsPayables.AddAsync(accountsPayable, cancellationToken);
                }
            }
        }
    }
}