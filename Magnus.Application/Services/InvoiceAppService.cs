using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class InvoiceAppService(
    IUnitOfWork unitOfWork,
    IInvoiceService invoiceService) : IInvoiceAppService
{
    public async Task AddInvoiceAsync(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = request.MapToEntity();
        await invoiceService.CreateInvoiceAsync(invoice, cancellationToken);
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
        unitOfWork.Invoices.Update(invoiceDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<InvoiceResponse>> GetInvoicesAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Invoices.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<InvoiceResponse>> GetInvoicesByFilterAsync(GetInvoiceFilter filter,
        CancellationToken cancellationToken)
    {
        var invoices = 
            await unitOfWork.Invoices.GetAllByExpressionAsync(x =>
                (x.DateEntry.Date >= filter.InitialDate)
                && (x.DateEntry.Date <= filter.FinalDate)
                && (filter.Number == 0 || x.Number == filter.Number)
                && (filter.Serie == 0 || x.Serie == filter.Serie)
                && (string.IsNullOrEmpty(filter.Key) || x.Key == filter.Key)
                && (filter.SupplierId == Guid.Empty || x.SupplierId == filter.SupplierId), cancellationToken);
        return invoices.MapToResponse();
    }

    public async Task<InvoiceResponse> GetInvoiceByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var invoice = await unitOfWork.Invoices.GetByIdAsync(id, cancellationToken);
        if (invoice is null)
            throw new EntityNotFoundException(id);
        return invoice.MapToResponse();
    }

    public async Task DeleteInvoiceAsync(Guid id, CancellationToken cancellationToken)
    {
        var invoiceDb = await unitOfWork.Invoices.GetByIdAsync(id, cancellationToken);
        if (invoiceDb is null)
            throw new EntityNotFoundException(id);
        await invoiceService.DeleteInvoiceAsync(invoiceDb, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}