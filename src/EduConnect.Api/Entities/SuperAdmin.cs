namespace EduConnect.Api.Entities;

public class SuperAdmin
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
}