namespace EduConnect.Api.Exceptions;

public class AdminNotFoundException(Guid id) 
    : Exception($"Admin with id: {id} is not found")
{
    public Guid Id { get; set; } = id;
}