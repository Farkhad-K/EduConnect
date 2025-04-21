namespace EduConnect.Api.Mappers.AttendanceMappers;

public static class EntityToDtos
{
    public static Entities.Attendance ToEntity(this Dtos.AttendanceDtos.CreateAttendance dto)
        => new()
        {
            Id = Guid.NewGuid(),
            // Date = DateOnly.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd")),
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            AttendanceStatus = dto.AttendanceStatus switch 
            {
                Dtos.AttendanceDtos.EAttendanceStatus.Present => Entities.EAttendanceStatus.Present,
                Dtos.AttendanceDtos.EAttendanceStatus.Absent => Entities.EAttendanceStatus.Absent,
                Dtos.AttendanceDtos.EAttendanceStatus.Tardy => Entities.EAttendanceStatus.Tardy,
                _=> throw new Exception($"{typeof(Dtos.AttendanceDtos.EAttendanceStatus).Name} value {dto.AttendanceStatus} not supported")
            },
            StudentId = dto.StudentId,
            ClassId = dto.ClassId
        };

    public static Dtos.AttendanceDtos.Attendance ToDto(this Entities.Attendance entity)
        => new()
        {
            StudentId = entity.StudentId,
            StudentName = entity.Student?.Name ?? "Unknown",
            Id = entity.Id,
            Date = entity.Date,
            AttendanceStatus = entity.AttendanceStatus switch
            {
                Entities.EAttendanceStatus.Present => Dtos.AttendanceDtos.EAttendanceStatus.Present,
                Entities.EAttendanceStatus.Absent => Dtos.AttendanceDtos.EAttendanceStatus.Absent,
                Entities.EAttendanceStatus.Tardy => Dtos.AttendanceDtos.EAttendanceStatus.Tardy,
                _ => throw new Exception($"{typeof(Entities.EAttendanceStatus).Name} value {entity.AttendanceStatus} not supported")
            },
            ClassId = entity.ClassId,
            ClassName = entity.Class?.Name ?? "Unknown"
        };
}