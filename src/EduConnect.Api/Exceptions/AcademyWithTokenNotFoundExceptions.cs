namespace EduConnect.Api.Exceptions;

public class AcademyWithTokenNotFoundException(
    string uniqueToken) : Exception
{
    public string UniqueToken { get; set; } = uniqueToken;
}