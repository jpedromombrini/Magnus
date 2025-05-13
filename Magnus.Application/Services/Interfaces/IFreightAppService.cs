using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface IFreightAppService
{
    Task AddFreightAsync(CreateFreightRequest request, CancellationToken cancellationToken);
    Task UpdateFreightAsync(Guid id, UpdateFreightRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<FreightResponse>> GetFreightsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<FreightResponse>> GetFreightsByFilterAsync(Expression<Func<Freight, bool>> predicate, CancellationToken cancellationToken);
    Task<FreightResponse> GetFreightByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteFreightAsync(Guid id, CancellationToken cancellationToken);
}