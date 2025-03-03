using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface ILaboratoryAppService
{
    Task AddLaboratoryAsync(CreateLaboratoryRequest request, CancellationToken cancellationToken);
    Task UpdateLaboratoryAsync(Guid id, UpdateLaboratoryRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<LaboratoryResponse>> GetLaboratoriesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<LaboratoryResponse>> GetLaboratoriesByFilterAsync(Expression<Func<Laboratory, bool>> predicate, CancellationToken cancellationToken);
    Task<LaboratoryResponse> GetLaboratoryByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteLaboratoryAsync(Guid id, CancellationToken cancellationToken);
}