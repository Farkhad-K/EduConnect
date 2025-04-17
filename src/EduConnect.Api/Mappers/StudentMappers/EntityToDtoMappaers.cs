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
            ClassIds = entity.Classes.Select(c => c.Id).ToList()
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