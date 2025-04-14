namespace EduConnect.Api.Mappers.ClassMappers;

public static class EntityToDtoMappers
{
    public static Entities.Class ToEntity(this Dtos.ClassDtos.CreateClass dto)
     => new()
     {
         Id = Guid.NewGuid(),
         Name = dto.Name,
         StartTime = dto.StartTime,
         EndTime = dto.EndTime,  // Remove this line
         TeacherId = dto.TeacherId,
         Schedule = dto.Schedule switch
         {
             Dtos.ClassDtos.EClassSchedule.Odd => Entities.EClassSchedule.Odd,
             Dtos.ClassDtos.EClassSchedule.Even => Entities.EClassSchedule.Even,
             _ => throw new Exception($"{typeof(Dtos.ClassDtos.EClassSchedule).Name} value {dto.Schedule} not supported")
         }
     };

    public static Dtos.ClassDtos.Class ToDto(this Entities.Class entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Schedule = entity.Schedule switch
            {
                Entities.EClassSchedule.Odd => Dtos.ClassDtos.EClassSchedule.Odd,
                Entities.EClassSchedule.Even => Dtos.ClassDtos.EClassSchedule.Even,
                _ => throw new Exception($"{typeof(Entities.EClassSchedule).Name} value {entity.Schedule} not supported")
            }
        };
}