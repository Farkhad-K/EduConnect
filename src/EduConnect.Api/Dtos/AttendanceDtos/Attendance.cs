namespace EduConnect.Api.Dtos.AttendanceDtos;

public class Attendance
{
    public Guid StudentId { get; set; }
    public string? StudentName { get; set; }
    public Guid Id { get; set; }
    public EAttendanceStatus AttendanceStatus { get; set; }
    public DateOnly Date { get; set; }
    public Guid ClassId { get; set; }
    public string? ClassName { get; set; }
}