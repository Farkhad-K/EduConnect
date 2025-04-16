using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EduConnect.Api.Abstractions;
using EduConnect.Api.Dtos;
using EduConnect.Api.Entities;
using EduConnect.Api.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace EduConnect.Api.Services;

public class AuthService(
    IUserRepository userRepository,
    IAcademyRepository academyRepository,
    ITokensForTeachersRepository tokensForTeachersRepository,
    IConfiguration config) : IAuthService
{
    public async Task<string> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email!)
            ?? throw new UnauthorizedAccessException("Invalid credentials");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash!))
            throw new UnauthorizedAccessException("Invalid credentials");

        return GenerateJwtToken(user);
    }

    public async Task<string> RegisterTeacherAsync(RegisterTeacherRequest request)
    {   
        var existingUser = await userRepository.GetByEmailAsync(request.Email!);
        if (existingUser != null)
            throw new InvalidOperationException("Email already registered");

        var token = await tokensForTeachersRepository.GetTokenAsync(request.TokenForTeacher!) 
            ?? throw new InvalidOperationException("Invalid Token");
        
        var teacher = new Teacher
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = Entities.EUserRole.Teacher,
            AcademyId = token.AcademyId
        };

        await userRepository.AddAsync(teacher);
        return GenerateJwtToken(teacher);
    }

    public async Task<string> RegisterAdminAsync(RegisterAdminRequest request)
    {
        var existingUser = await userRepository.GetByEmailAsync(request.Email!);
        if (existingUser != null)
            throw new InvalidOperationException("Email already registered");

        var academy = await academyRepository.GetByTokenAsync(request.TokenOfAcademy!) 
            ?? throw new InvalidOperationException("Invalid Academy Token");    

        var admin = new Admin
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = Entities.EUserRole.Admin,
            TokenOfAcademy = request.TokenOfAcademy,
            AcademyId = academy.Id
        };

        await userRepository.AddAsync(admin);
        return GenerateJwtToken(admin);
    }


    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await userRepository.GetByEmailAsync(request.Email!);
        if (existingUser != null)
            throw new InvalidOperationException("Email already registered");

        var entityRole = MapDtoRoleToEntity(request.Role);

        UserBase user = entityRole switch
        {
            EduConnect.Api.Entities.EUserRole.Admin => new Admin
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = EduConnect.Api.Entities.EUserRole.Admin
            },

            EduConnect.Api.Entities.EUserRole.Teacher => new Teacher
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = EduConnect.Api.Entities.EUserRole.Teacher
            },

            EduConnect.Api.Entities.EUserRole.Parent => new Parent
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = EduConnect.Api.Entities.EUserRole.Parent
            },
            _ => throw new ArgumentOutOfRangeException()
        };



        await userRepository.AddAsync(user);
        return GenerateJwtToken(user);
    }

    private EduConnect.Api.Entities.EUserRole MapDtoRoleToEntity(EduConnect.Api.Dtos.EUserRole dtoRole)
    {
        return dtoRole switch
        {
            EduConnect.Api.Dtos.EUserRole.Admin => EduConnect.Api.Entities.EUserRole.Admin,
            EduConnect.Api.Dtos.EUserRole.Teacher => EduConnect.Api.Entities.EUserRole.Teacher,
            EduConnect.Api.Dtos.EUserRole.Parent => EduConnect.Api.Entities.EUserRole.Parent,
            _ => throw new ArgumentOutOfRangeException(nameof(dtoRole), dtoRole, null)
        };
    }


    private string GenerateJwtToken(UserBase user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Name!),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!)
        };

        if (user is Admin admin && admin.AcademyId.HasValue)
            claims.Add(new Claim("AcademyId", admin.AcademyId.Value.ToString()));
        if (user is Teacher teacher && teacher.AcademyId.HasValue)
            claims.Add(new Claim("AcademyId", teacher.AcademyId.Value.ToString()));

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMonths(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
