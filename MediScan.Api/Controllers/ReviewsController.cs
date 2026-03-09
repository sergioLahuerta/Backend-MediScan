using Microsoft.AspNetCore.Mvc;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Authorization.Authorize]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _Service;

    public ReviewsController(IReviewService Service)
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


