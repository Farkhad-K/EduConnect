using EduConnect.Api.Dtos.TeacherDtos;

namespace EduConnect.Api.Mappers.TeacherMappers;

public static class EntityToDtoMappers
{
    public static Dtos.TeacherDtos.Teacher ToDto(this Entities.Teacher entity) 
        => new()
        {
            Id = entity.Id,
            Name = entity.Name!,
            Email = entity.Email,
            Classes = entity.Classes.Select(c => new ClassesOfTeacher 
            { 
                ClassId = c.Id,
                ClassName = c.Name! 
            }).ToList()
        };
}