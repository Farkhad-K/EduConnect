using EduConnect.Api.Utilities;

namespace EduConnect.Api.Mappers.AcademyMappers;

public static class EntityToDtoMappers
{
    public static Dtos.AcademyDtos.Academy ToDto(this Entities.Academy entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            UniqueToken = entity.UniqueToken!
        };

    public static Entities.Academy ToEntity(this Dtos.AcademyDtos.CreateAcademy dto)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            UniqueToken = TokenGenerator.GenerateToken()
        };

    public static Entities.Academy UpdatedToEntity(this Dtos.AcademyDtos.UpdateAcademy dto)
        => new()
        {
            Name = dto.Name,
            Address = dto.Address
        };
}