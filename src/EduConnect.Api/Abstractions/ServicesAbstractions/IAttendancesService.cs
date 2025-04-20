using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions.ServicesAbstractions;

public interface IAttendancesService
{
    ValueTask<Attendance> AddAttendanceAsync(Attendance attendance, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Attendance>> GetAttendancesByClassAndDateAsync(Guid classId, DateOnly date, CancellationToken cancellationToken = default);
    ValueTask<Attendance?> GetAttendanceByStudentClassDateAsync(Guid studentId, Guid classId, DateOnly date, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Attendance>> GetAllAttendancesOfAStudentAsync(Guid studentId, CancellationToken cancellationToken = default);
}