using Microsoft.AspNetCore.Mvc;
using MediScan.Application.Services;
using MediScan.Application.DTOs.Users;
using Microsoft.AspNetCore.Authorization;

namespace MediScan.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] UserLoginDto loginDto)
    {
        try
        {
            var token = _authService.Login(loginDto);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Error generating the token: " + ex.Message });
        }
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public IActionResult Register([FromBody] UserRegisterDto registerDto)
    {
        try
        {
            var token = _authService.Register(registerDto);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("ForgotPassword")]
    [AllowAnonymous]
    public IActionResult ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        try
        {
            _authService.ForgotPassword(forgotPasswordDto);
            return Ok(new { Message = "Password updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
