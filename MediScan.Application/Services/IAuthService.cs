using MediScan.Application.DTOs.Users;

namespace MediScan.Application.Services;

public interface IAuthService
{
    string Login(UserLoginDto loginDtoIn);
    string Register(UserRegisterDto userDtoIn);
    string GenerateToken(UserDto userDtoOut);
    void ForgotPassword(ForgotPasswordDto forgotPasswordDto);
}
