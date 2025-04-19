using EduConnect.Api.Dtos.StudentDtos;
using EduConnect.Api.Utilities;

namespace EduConnect.Api.Mappers.StudentMappers;

public static class EntityToDtoMappers
{
    public static Dtos.StudentDtos.Student ToDto(this Entities.Student entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name!,
            UniqueToken = entity.UniqueToken!,
            Classes = entity.Classes.Select(c => new ClassesOfStudent
            {
                TeacherId = c.TeacherId!.Value,
                TeacherName = c.Teacher?.Name ?? "Unknown",
                ClassId = c.Id,
                ClassName = c.Name!,
                StartTime = c.StartTime,
                EndTime = c.EndTime
            }).ToList()
        };

    public static Entities.Student ToEntity(this Dtos.StudentDtos.CreateStudent dto, Guid academyId)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            UniqueToken = TokenGenerator.GenerateToken(),
            AcademyId = academyId
        };
}