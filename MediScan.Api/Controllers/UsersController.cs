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
            CreatedAt = u.CreatedAt,
            ProfileImageUrl = u.ProfileImageUrl
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
            CreatedAt = user.CreatedAt,
            ProfileImageUrl = user.ProfileImageUrl
        };
        return Ok(userDto);
    }

    [HttpPost("profile-image")]
    public async Task<IActionResult> UploadProfileImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);
        var user = await _repository.GetByIdAsync(userId);
        if (user == null) return NotFound();

        // Create directory if not exists
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        // Save file
        var fileName = $"{userId}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Update database
        var relativeUrl = $"/uploads/profiles/{fileName}";
        user.ProfileImageUrl = relativeUrl;
        await _repository.UpdateAsync(user);

        return Ok(new { ProfileImageUrl = relativeUrl });
    }
}
