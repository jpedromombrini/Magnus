using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Servicos;

namespace Magnus.Application.Services;

public class InvoiceAppService(
    IUnitOfWork unitOfWork,
    IInvoiceService invoiceService,
    IMapper mapper) : IInvoiceAppService
{
    public async Task AddInvoiceAsync(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = mapper.Map<Invoice>(request);
        var accountsPayables = mapper.Map<List<AccountsPayable>>(request.AccountsPayables);
        if (invoice is null)
            throw new ApplicationException("Não foi possível converter o objeto");
        var invoiceDb = await unitOfWork.Invoices.GetByExpressionAsync(x => x.SupplierId == request.SupplierId
                                                                            && x.Number == request.Number
                                                                            && x.Serie == request.Serie, cancellationToken);
        if (invoiceDb is not null)
            throw new ApplicationException("Já existe uma NF com esses dados");

        var supplier = await unitOfWork.Suppliers.GetByIdAsync(invoice.SupplierId, cancellationToken);
        if (supplier is null)
            throw new EntityNotFoundException(invoice.SupplierId);
        
        invoice.SupplierName = supplier.Name;
        decimal totalItemsValue = invoice.Items
            .Where(x => x.Bonus == false)
            .Sum(x => x.TotalValue);
        decimal totalFinantialValue = accountsPayables.Sum(x => x.Value);
        if (totalItemsValue == 0 || totalItemsValue != invoice.Value)
            throw new ApplicationException("O valor total dos items difere do valor total da nota");
        if(totalFinantialValue == 0 || totalFinantialValue != invoice.Value)
            throw new ApplicationException("O valor total dos pagamentos difere do valor total da nota");
        await invoiceService.CreateInvoiceAsync(invoice,accountsPayables, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateInvoiceAsync(Guid id, UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoiceDb = await unitOfWork.Invoices.GetByIdAsync(id, cancellationToken);
        if (invoiceDb is null)
            throw new EntityNotFoundException(id);
        invoiceDb.SetNumber(request.Number);
        invoiceDb.SetSerie(request.Serie);
        invoiceDb.SetKey(request.Key);
        invoiceDb.SetDateEntry(request.DateEntry);
        invoiceDb.SetDate(request.Date);
        invoiceDb.SetSupplierId(request.SupplierId);
        invoiceDb.SetSupplierName(request.SupplierName);
        invoiceDb.SetFreight(request.Freight);
        invoiceDb.SetDeduction(request.Deduction);
        invoiceDb.SetValue(request.Value);
        invoiceDb.SetObservation(request.Observation);
        invoiceDb.SetInvoiceSituation(request.InvoiceSituation);
        invoiceDb.SetDoctorId(request.DoctorId);
        unitOfWork.Invoices.UpdateAsync(invoiceDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<InvoiceResponse>> GetInvoicesAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<InvoiceResponse>>(await unitOfWork.Invoices.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<InvoiceResponse>> GetInvoicesByFilterAsync(Expression<Func<Invoice, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<InvoiceResponse>>(
            await unitOfWork.Invoices.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<InvoiceResponse> GetInvoiceByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<InvoiceResponse>(await unitOfWork.Invoices.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteInvoiceAsync(Guid id, CancellationToken cancellationToken)
    {
        var invoiceDb = await unitOfWork.Invoices.GetByIdAsync(id, cancellationToken);
        if (invoiceDb is null)
            throw new EntityNotFoundException(id);
        await invoiceService.DeleteInvoiceAsync(invoiceDb, cancellationToken);
    }
}