namespace EduConnect.Api.Exceptions;

public class StudentWithTokenNotFoundException(string token) 
    : Exception($"Student with token: {token} is not found")
{
    public string? Token { get; set; } = token;
}