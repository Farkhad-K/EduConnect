namespace EduConnect.Api.Dtos.StudentDtos;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UniqueToken { get; set; } = string.Empty;
    public List<ClassesOfStudent> Classes { get; set; } = [];
}

public class ClassesOfStudent
{
    public Guid TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public Guid ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}