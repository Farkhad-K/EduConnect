namespace EduConnect.Api.Dtos;

public class RegisterAdminRequest : RegisterRequest
{
    public string? TokenOfAcademy { get; set; }
}