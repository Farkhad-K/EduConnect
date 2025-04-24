namespace EduConnect.Api.Exceptions;

public class ParentNotFoundException(Guid id) 
    : Exception($"Parent with id: {id} is not found")
{
    public Guid Id { get; set; } = id;
}