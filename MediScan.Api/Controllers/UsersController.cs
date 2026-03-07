using Microsoft.AspNetCore.Mvc;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Entities;
using MediScan.Application.DTOs.Users;
using Microsoft.AspNetCore.Authorization;

namespace MediScan.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _repository.GetAllAsync();
        var userDtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            Email = u.Email,
            RoleId = u.RoleId,
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt
        });
        return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            RoleId = user.RoleId,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
        return Ok(userDto);
    }
}
