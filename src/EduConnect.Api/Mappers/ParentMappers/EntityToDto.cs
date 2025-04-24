using EduConnect.Api.Dtos.ParentDtos;

namespace EduConnect.Api.Mappers.ParentMappers;

public static class EntityToDto
{
    public static Dtos.ParentDtos.Parent ToDto(this Entities.Parent entity)
        => new() 
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Children = entity.Students.Select(c => new Child
            {
                StudentId = c.Id,
                Name = c.Name
            }).ToList()
        };
}

/*
Classes = entity.Classes.Select(c => new ClassesOfStudent
            {
                TeacherId = c.TeacherId!.Value,
                TeacherName = c.Teacher?.Name ?? "Unknown",
                ClassId = c.Id,
                ClassName = c.Name!,
                StartTime = c.StartTime,
                EndTime = c.EndTime
            }).ToList()
*/