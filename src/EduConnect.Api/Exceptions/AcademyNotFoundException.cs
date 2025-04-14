namespace EduConnect.Api.Exceptions;

public class AcademyNotFoundException(Guid id) 
    : Exception($"Academy with id: {id} is not found")
{
    public Guid Id { get; set; } = id;
}