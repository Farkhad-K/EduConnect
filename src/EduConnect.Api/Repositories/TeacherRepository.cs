using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Data;
using EduConnect.Api.Entities;
using EduConnect.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Repositories;

public class TeacherRepository(
    IEduConnectDbContext context) : ITeacherRepository
{
    public async ValueTask<IEnumerable<Teacher>> GetAllAsync(Guid academyId, CancellationToken cancellationToken = default)
        => await context.Teachers.AsNoTracking()
            .Where(a => a.AcademyId == academyId)
            .Include(c => c.Classes)
            .ToListAsync(cancellationToken);

    public async ValueTask<Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Teachers.Include(c => c.Classes).FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
            ?? throw new TeacherNotFoundException(id);

    public async ValueTask<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingTeacher = await GetByIdAsync(id, cancellationToken);

        if (existingTeacher == null)
            return false;

        context.Teachers.Remove(existingTeacher);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
