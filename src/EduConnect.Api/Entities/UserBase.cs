namespace EduConnect.Api.Entities;

public abstract class UserBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public EUserRole Role { get; set; }
}
