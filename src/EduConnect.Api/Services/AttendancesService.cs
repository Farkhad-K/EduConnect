using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Entities;

namespace EduConnect.Api.Services;

public class AttendancesService(
    IAttendanceRepository repository) : IAttendancesService
{
    public async ValueTask<Attendance> AddAttendanceAsync(Attendance attendance, CancellationToken cancellationToken = default)
        => await repository.AddAsync(attendance, cancellationToken);

    public async ValueTask<IEnumerable<Attendance>> GetAllAttendancesOfAStudentAsync(
        Guid studentId, 
        CancellationToken cancellationToken = default)
            => await repository.GetByStudentAsync(studentId, cancellationToken);

    public async ValueTask<Attendance?> GetAttendanceByStudentClassDateAsync(
        Guid studentId, 
        Guid classId, 
        DateOnly date, 
        CancellationToken cancellationToken = default)
            => await repository.GetByStudentClassDateAsync(studentId, classId, date, cancellationToken);

    public async ValueTask<IEnumerable<Attendance>> GetAttendancesByClassAndDateAsync(
        Guid classId, 
        DateOnly date, 
        CancellationToken cancellationToken = default)
            => await repository.GetByClassAndDateAsync(classId, date, cancellationToken);
}
