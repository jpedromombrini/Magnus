using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace Magnus.Application.Services;

public class DoctorAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IDoctorAppService
{
    public async Task AddDoctorAsync(CreateDoctorRequest request, CancellationToken cancellationToken)
    {
        var doctorDb = await unitOfWork.Doctors.GetByExpressionAsync(
            x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if (doctorDb is not null)
            throw new ApplicationException("Já existe um médico com esse nome");
        await unitOfWork.Doctors.AddAsync(mapper.Map<Doctor>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateDoctorAsync(Guid id, UpdateDoctorRequest request, CancellationToken cancellationToken)
    {
        var doctorDb = await unitOfWork.Doctors.GetByIdAsync(id, cancellationToken);
        if (doctorDb is null)
            throw new EntityNotFoundException(id);
        doctorDb.SetName(request.Name);
        doctorDb.SetCrm(request.Crm);
        unitOfWork.Doctors.Update(doctorDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<DoctorResponse>>(await unitOfWork.Doctors.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<DoctorResponse>> GetDoctorsByFilterAsync(Expression<Func<Doctor, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<DoctorResponse>>(
            await unitOfWork.Doctors.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<DoctorResponse> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<DoctorResponse>(await unitOfWork.Doctors.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteDoctorAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctorDb = await unitOfWork.Doctors.GetByIdAsync(id, cancellationToken);
        if (doctorDb is null)
            throw new EntityNotFoundException(id);
        unitOfWork.Doctors.Delete(doctorDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}