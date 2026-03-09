using Microsoft.AspNetCore.Mvc;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Authorization.Authorize]
[Route("api/[controller]")]
public class ProfessionalsController : ControllerBase
{
    private readonly IProfessionalService _Service;

    public ProfessionalsController(IProfessionalService Service)
    {
        _Service = Service;
    }

    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _Service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _Service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}


