using EduConnect.Api.Utilities;

namespace EduConnect.Api.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? UniqueToken { get; set; }

    public Guid? AcademyId { get; set; }
    public Academy? Academy { get; set; }
    public ICollection<Class> Classes { get; set; } = new List<Class>();
    public ICollection<Parent> Parents { get; set; } = new List<Parent>();
    public ICollection<Attendance> Attendances { get; set; } = [];
}