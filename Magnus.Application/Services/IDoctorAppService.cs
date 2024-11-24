using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services;

public interface IDoctorAppService
{
    Task AddDoctorAsync(CreateDoctorRequest request, CancellationToken cancellationToken);
    Task UpdateDoctorAsync(Guid id, UpdateDoctorRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<DoctorResponse>> GetDoctorsByFilterAsync(Expression<Func<Doctor, bool>> predicate, CancellationToken cancellationToken);
    Task<DoctorResponse> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteDoctorAsync(Guid id, CancellationToken cancellationToken);
}