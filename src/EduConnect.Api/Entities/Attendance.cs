namespace EduConnect.Api.Entities;

public class Attendance
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public EAttendanceStatus AttendanceStatus { get; set; }
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid ClassId { get; set; }
    public Class? Class { get; set; }
}
