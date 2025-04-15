namespace EduConnect.Api.Mappers.TeacherMappers;

public static class EntityToDtoMappers
{
    public static Dtos.TeacherDtos.Teacher ToDto(this Entities.UserBase entity) 
        => new()
        {
            Id = entity.Id,
            Name = entity.Name!,
            Email = entity.Email
        };
}