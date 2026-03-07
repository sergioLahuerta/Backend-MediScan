using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Authorization.Authorize]
[Route("api/[controller]")]
public class TreatmentsController : ControllerBase
{
    private readonly ITreatmentService _Service;

    public TreatmentsController(ITreatmentService Service)
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


