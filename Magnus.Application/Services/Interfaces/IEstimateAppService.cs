using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface IEstimateAppService
{
    Task AddEstimateAsync(CreateEstimateRequest request, CancellationToken cancellationToken);
    Task UpdateEstimateAsync(Guid id, UpdateEstimateRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<EstimateResponse>> GetEstimatesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<EstimateResponse>> GetEstimatesByFilterAsync(Expression<Func<Estimate, bool>> predicate, CancellationToken cancellationToken);
    Task<EstimateResponse> GetEstimateByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteEstimateAsync(Guid id, CancellationToken cancellationToken);
    Task<byte[]> CreatePdf(Guid id, CancellationToken cancellationToken);
}