namespace EduConnect.Api.Dtos.ClassDtos;

public class Class
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public EClassSchedule Schedule { get; set; }
}
