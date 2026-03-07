using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MediScan.Application.DTOs.Users;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Entities;
using BCrypt.Net;

namespace MediScan.Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;

    public AuthService(IConfiguration configuration, IUserRepository repository)
    {
        _configuration = configuration;
        _repository = repository;
    }

    public string Login(UserLoginDto loginDtoIn)
    {
        var users = _repository.GetAllAsync().GetAwaiter().GetResult();
        var user = users.FirstOrDefault(u => u.Email == loginDtoIn.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDtoIn.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            RoleId = user.RoleId,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };

        return GenerateToken(userDto);
    }

    public string Register(UserRegisterDto userDtoIn)
    {
        var users = _repository.GetAllAsync().GetAwaiter().GetResult();
        if (users.Any(u => u.Email == userDtoIn.Email))
            throw new Exception("Email is already registered");

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = userDtoIn.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDtoIn.Password),
            RoleId = userDtoIn.RoleId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _repository.AddAsync(newUser).GetAwaiter().GetResult();

        var userDto = new UserDto
        {
            Id = newUser.Id,
            Email = newUser.Email,
            RoleId = newUser.RoleId,
            IsActive = newUser.IsActive,
            CreatedAt = newUser.CreatedAt
        };

        return GenerateToken(userDto);
    }

    public string GenerateToken(UserDto userDtoOut)
    {
        var keyString = _configuration["Jwt:Key"] ?? "super_secret_key_mediscan_min_32_characters_1234!";
        var issuer = _configuration["Jwt:Issuer"] ?? "MediScanApi";

        var key = Encoding.UTF8.GetBytes(keyString);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = issuer,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDtoOut.Id.ToString()),
                new Claim(ClaimTypes.Email, userDtoOut.Email),
                new Claim(ClaimTypes.Role, userDtoOut.RoleId.ToString() ?? "0")
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
