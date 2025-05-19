using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class PaymentAppService(
    IUnitOfWork unitOfWork) : IPaymentAppService
{
    public async Task AddPaymentAsync(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var paymentDb = await unitOfWork.Payments.GetByExpressionAsync(
            x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (paymentDb is not null)
            throw new ApplicationException("Já existe um pagamento com esse nome");
        await unitOfWork.Payments.AddAsync(request.MapToEntity(), cancellationToken);
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
        return (await unitOfWork.Payments.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<PaymentResponse>> GetPaymentsByFilterAsync(Expression<Func<Payment, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await unitOfWork.Payments.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<PaymentResponse> GetPaymentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var payment =  await unitOfWork.Payments.GetByIdAsync(id, cancellationToken);
        if (payment is null)
            throw new EntityNotFoundException(id);
        return payment.MapToResponse();
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