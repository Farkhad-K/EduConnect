using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions.RepositoriesAbstractions;

public interface IAttendanceRepository
{
    ValueTask<Attendance> AddAsync(Attendance attendance, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Attendance>> GetByClassAndDateAsync(Guid classId, DateOnly date, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Attendance>> GetByStudentAsync(Guid studentId, CancellationToken cancellationToken = default);
    ValueTask<Attendance?> GetByStudentClassDateAsync(Guid studentId, Guid classId, DateOnly date, CancellationToken cancellationToken = default);
}
