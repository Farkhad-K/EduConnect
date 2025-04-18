namespace EduConnect.Api.Dtos.TeacherDtos;

public class Teacher
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public List<ClassesOfTeacher> Classes { get; set; } = [];
}

public class ClassesOfTeacher
{
    public Guid ClassId { get; set; }
    public string? ClassName { get; set; }
}