using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Data;
using EduConnect.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Repositories;

public class AttendanceRepository(
IEduConnectDbContext context) : IAttendanceRepository
{
    public async ValueTask<Attendance> AddAsync(Attendance attendance, CancellationToken cancellationToken = default)
    {
        var entry = context.Attendances.Add(attendance);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async ValueTask<IEnumerable<Attendance>> GetByClassAndDateAsync(Guid classId, DateOnly date, CancellationToken cancellationToken = default)
        => await context.Attendances.AsNoTracking()
            .Where(c => c.ClassId == classId && c.Date == date)
            .Include(s => s.Student)
            .Include(c => c.Class)
            .ToListAsync(cancellationToken);

    public async ValueTask<IEnumerable<Attendance>> GetByStudentAsync(Guid studentId, CancellationToken cancellationToken = default)
        => await context.Attendances.AsNoTracking()
            .Where(s => s.StudentId == studentId)
            .Include(c => c.Class)
            .Include(c => c.Student)
            .ToListAsync(cancellationToken);

    public async ValueTask<Attendance?> GetByStudentClassDateAsync(Guid studentId, Guid classId, DateOnly date, CancellationToken cancellationToken = default)
        => await context.Attendances
            .Include(c => c.Class)
            .Include(s => s.Student)
            .FirstOrDefaultAsync(
                a => a.StudentId == studentId &&
                a.ClassId == classId &&
                a.Date == date,
                cancellationToken);
}