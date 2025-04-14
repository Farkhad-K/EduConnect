namespace EduConnect.Api.Dtos.StudentDtos;

public class CreateStudent
{
    public string Name { get; set; } = string.Empty;
    public List<Guid> ClassIds { get; set; } = new(); // Optional: Attach to existing classes
}