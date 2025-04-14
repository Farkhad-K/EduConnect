using EduConnect.Api.Utilities;

namespace EduConnect.Api.Entities;

public class Academy
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? UniqueToken { get; set; }

    public ICollection<Admin> Admins { get; set; } = new List<Admin>();
    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Class> Classes { get; set; } = new List<Class>();
    public ICollection<TokenForTeachers> TeacherTokens { get; set; } = new List<TokenForTeachers>();
}