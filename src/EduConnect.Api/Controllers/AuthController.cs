using EduConnect.Api.Abstractions;
using EduConnect.Api.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    IAuthService authService, 
    IValidator<RegisterRequest> registerValidator, 
    IValidator<RegisterAdminRequest> registerAdminValidator,
    IValidator<RegisterTeacherRequest> registerTeacherValidator,
    IValidator<LoginRequest> loginValidator) : ControllerBase
{
    /// <summary>
    /// Registers a new user (non-admin).
    /// </summary>
    [HttpPost("register-parent")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request)
    {
        var validationResult = await registerValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var user = await authService.RegisterAsync(request);
        return CreatedAtAction(nameof(RegisterUser), new { email = user.Email }, new { user.Id, user.Email });
    }

    /// <summary>
    /// Registers a new admin with an associated academy.
    /// </summary>
    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminRequest request)
    {
        var validationResult = await registerAdminValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var token = await authService.RegisterAdminAsync(request);
        return Ok(new { Token = token });
    }

    [HttpPost("register-teacher")]
    public async Task<IActionResult> RegisterTeacher([FromBody] RegisterTeacherRequest request)
    {
        var validationResult = await registerTeacherValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var token = await authService.RegisterTeacherAsync(request);
        return Ok(new { Token = token });
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var validationResult = await loginValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var token = await authService.LoginAsync(request);
        return Ok(new { Token = token });
    }
}
