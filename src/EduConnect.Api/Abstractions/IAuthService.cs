using EduConnect.Api.Dtos;
using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions;

public interface IAuthService
{
    Task<UserBase> RegisterAsync(RegisterRequest request);
    Task<string> RegisterAdminAsync(RegisterAdminRequest request);
    Task<string> RegisterTeacherAsync(RegisterTeacherRequest request);
    Task<string> LoginAsync(LoginRequest request);
}
