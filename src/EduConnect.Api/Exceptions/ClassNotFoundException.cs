namespace EduConnect.Api.Exceptions;

public class ClassNotFoundException(Guid id) 
    : Exception($"Class with id: {id} is not found")
{
    public Guid Id { get; set; } = id;
}