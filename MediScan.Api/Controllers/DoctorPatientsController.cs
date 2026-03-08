using Microsoft.AspNetCore.Mvc;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Authorization.Authorize]
[Route("api/[controller]")]
public class DoctorPatientsController : ControllerBase
{
    private readonly IDoctorPatientService _Service;

    public DoctorPatientsController(IDoctorPatientService Service)
    {
        _Service = Service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _Service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(object id)
    {
        var result = await _Service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}


