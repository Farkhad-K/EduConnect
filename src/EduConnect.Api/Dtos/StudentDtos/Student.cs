namespace EduConnect.Api.Dtos.StudentDtos;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UniqueToken { get; set; } = string.Empty;
    public List<Guid> ClassIds { get; set; } = [];
}