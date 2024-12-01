using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class PaymentAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IPaymentAppService
{
    public async Task AddPaymentAsync(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.Payments.AddAsync(mapper.Map<Payment>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePaymentAsync(Guid id, UpdatePaymentRequest request, CancellationToken cancellationToken)
    {
        var paymentDb = await unitOfWork.Payments.GetByIdAsync(id, cancellationToken);
        if (paymentDb is null)
            throw new EntityNotFoundException(id);
        paymentDb.SetName(request.Name);
        unitOfWork.Payments.Update(paymentDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<PaymentResponse>> GetPaymentsAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<PaymentResponse>>(await unitOfWork.Payments.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<PaymentResponse>> GetPaymentsByFilterAsync(Expression<Func<Payment, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<PaymentResponse>>(
            await unitOfWork.Payments.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<PaymentResponse> GetPaymentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<PaymentResponse>(await unitOfWork.Payments.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeletePaymentAsync(Guid id, CancellationToken cancellationToken)
    {
        var paymentDb = await unitOfWork.Payments.GetByIdAsync(id, cancellationToken);
        if (paymentDb is null)
            throw new EntityNotFoundException(id);
        unitOfWork.Payments.Delete(paymentDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}