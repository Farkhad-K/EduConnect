namespace EduConnect.Api.Entities;

public class Class
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public EClassSchedule Schedule { get; set; }

    public Guid? AcademyId { get; set; }
    public Academy? Academy { get; set; }
    public Guid? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
