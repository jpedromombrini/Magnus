using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class DoctorController(
    IDoctorAppService doctorAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<DoctorResponse>> GetAllDoctorsAsync(CancellationToken cancellationToken)
    {
        return await doctorAppService.GetDoctorsAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<DoctorResponse>> GetDoctorsByFilterAsync([FromQuery] string filter,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filter))
            return [];
        return await doctorAppService.GetDoctorsByFilterAsync(x => x.Name.ToLower().Contains(filter.ToLower()),
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