using Microsoft.AspNetCore.Mvc;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Application.DTOs.Patients;
using Microsoft.AspNetCore.Authorization;

namespace MediScan.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly IPatientRepository _repository;

    public PatientsController(IPatientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _repository.GetAllAsync();
        var patientDtos = patients.Select(p => new PatientDto
        {
            UserId = p.UserId,
            FirstName = p.FirstName,
            LastName = p.LastName,
            DateOfBirth = p.DateOfBirth
        });
        return Ok(patientDtos);
    }
}
