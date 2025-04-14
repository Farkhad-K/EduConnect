namespace EduConnect.Api.Exceptions;

public class StudentNotFoundException(Guid id) 
    : Exception($"Student with id: {id} is not found")
{
    public Guid Id { get; set; } = id;
}