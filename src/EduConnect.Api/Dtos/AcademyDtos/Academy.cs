using EduConnect.Api.Entities;
using EduConnect.Api.Utilities;

namespace EduConnect.Api.Dtos.AcademyDtos;

public class Academy
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? UniqueToken { get; set; }
}