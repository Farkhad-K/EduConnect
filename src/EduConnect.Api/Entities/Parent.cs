namespace EduConnect.Api.Entities;

public class Parent : UserBase
{
    public ICollection<Student> Students { get; set; } = new List<Student>();
}