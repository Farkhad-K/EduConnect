namespace EduConnect.Api.Dtos.ParentDtos;

public class Parent
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public List<Child> Children { get; set; } = [];
}

public class Child
{
    public Guid StudentId { get; set; }
    public string? Name { get; set; }
}