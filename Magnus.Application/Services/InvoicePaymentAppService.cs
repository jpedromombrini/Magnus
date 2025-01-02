using AutoMapper;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;
public class InvoicePaymentAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IInvoicePaymentAppService
{
    public async Task<IEnumerable<InvoicePaymentResponse>> GetInvoicePaymentsAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<InvoicePaymentResponse>>(await unitOfWork.InvoicePayments.GetAllAsync(cancellationToken));
    }

    public async Task<InvoicePaymentResponse> GetInvoicePaymentsByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken)
    {
        return mapper.Map<InvoicePaymentResponse>(await unitOfWork.InvoicePayments.GetAllByExpressionAsync(x => x.InvoiceId == invoiceId, cancellationToken));
    }
}