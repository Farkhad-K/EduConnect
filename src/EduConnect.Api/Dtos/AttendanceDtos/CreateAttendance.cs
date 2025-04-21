namespace EduConnect.Api.Dtos.AttendanceDtos;

public class CreateAttendance
{
    public EAttendanceStatus AttendanceStatus { get; set; }
    public Guid StudentId { get; set; }
    public Guid ClassId { get; set; }
}
