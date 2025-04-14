namespace EduConnect.Api.Dtos;

public class RegisterRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public EUserRole Role { get; set; }
}
