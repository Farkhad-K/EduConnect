namespace EduConnect.Api.Dtos;

public class RegisterTeacherRequest : RegisterRequest
{
    public string? TokenForTeacher { get; set; }
}