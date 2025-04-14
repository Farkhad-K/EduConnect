namespace EduConnect.Api.Dtos.ClassDtos;

public class CreateClass
{
    public string? Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public EClassSchedule Schedule { get; set; }
    public Guid? TeacherId { get; set; }
}
