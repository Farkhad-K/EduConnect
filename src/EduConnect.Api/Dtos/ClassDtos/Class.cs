namespace EduConnect.Api.Dtos.ClassDtos;

public class Class
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public EClassSchedule Schedule { get; set; }
    public Guid? TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public List<StudentsOfClass> Students { get; set; } = [];
}

public class StudentsOfClass
{
    public Guid StudentId { get; set; }
    public string? StudnetName { get; set; }
}
