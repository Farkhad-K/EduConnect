namespace EduConnect.Api.Exceptions;

public class TeacherNotFoundException(Guid id) 
    : Exception($"Teacher with id: {id} is not found")
{
    public Guid Id { get; set; } = id;
}