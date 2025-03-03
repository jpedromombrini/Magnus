using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DoctorController(
    IDoctorAppService doctorAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<DoctorResponse>> GetAllDoctorsAsync(CancellationToken cancellationToken)
    {
        return await doctorAppService.GetDoctorsAsync(cancellationToken);
    }

    [HttpGet("getbyname")]
    public async Task<IEnumerable<DoctorResponse>> GetDoctorsByFilterAsync([FromQuery] string name,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            return [];
        return await doctorAppService.GetDoctorsByFilterAsync(x => x.Name.ToLower().Contains(name.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<DoctorResponse> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await doctorAppService.GetDoctorByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddDoctorAsync([FromBody] CreateDoctorRequest request, CancellationToken cancellationToken)
    {
        await doctorAppService.AddDoctorAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateDoctorAsync(Guid id, [FromBody] UpdateDoctorRequest request,
        CancellationToken cancellationToken)
    {
        await doctorAppService.UpdateDoctorAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteDoctorAsync(Guid id, CancellationToken cancellationToken)
    {
        await doctorAppService.DeleteDoctorAsync(id, cancellationToken);
    }
}