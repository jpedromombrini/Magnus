using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface IPaymentAppService
{
    Task AddPaymentAsync(CreatePaymentRequest request, CancellationToken cancellationToken);
    Task UpdatePaymentAsync(Guid id, UpdatePaymentRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<PaymentResponse>> GetPaymentsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<PaymentResponse>> GetPaymentsByFilterAsync(Expression<Func<Payment, bool>> predicate, CancellationToken cancellationToken);
    Task<PaymentResponse> GetPaymentByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeletePaymentAsync(Guid id, CancellationToken cancellationToken);
}