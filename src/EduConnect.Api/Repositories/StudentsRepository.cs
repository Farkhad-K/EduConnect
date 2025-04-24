using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Data;
using EduConnect.Api.Entities;
using EduConnect.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Repositories;

public class StudentsRepository(
    IEduConnectDbContext context,
    ILogger<StudentsRepository> logger) : IStudentsRepository
{
    public async ValueTask<Student> AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        var entry = context.Students.Add(student);

        await context.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    // public async ValueTask<Student> AppedToParentAsync(Guid studentId, Guid parentId, CancellationToken cancellationToken = default)
    // {
    //     var existingParent = await context.Parents.FirstOrDefaultAsync(p => p.Id == parentId, cancellationToken);
    //     if (existingParent == null) throw new ParentNotFoundException(parentId);
    // }

    public async ValueTask<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Students.Remove(new Student { Id = id });
            return await context.SaveChangesAsync(cancellationToken) is 1;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to delete student with id {Id}", id);
            throw new StudentNotFoundException(id);
        }
    }

    public async ValueTask<IEnumerable<Student>> GetAllAsync(Guid academyId, CancellationToken cancellationToken = default)
        => await context.Students.AsNoTracking().Where(a => a.AcademyId == academyId)
        .Include(c => c.Classes).ThenInclude(c => c.Teacher).ToListAsync(cancellationToken);

    public async ValueTask<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Students.Include(c => c.Classes)
        .ThenInclude(c => c.Teacher).FirstOrDefaultAsync(s => s.Id == id, cancellationToken)
        ?? throw new StudentNotFoundException(id);

    public async ValueTask<Student?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        => await context.Students.Include(c => c.Classes)
        .ThenInclude(c => c.Teacher).FirstOrDefaultAsync(s => s.UniqueToken == token, cancellationToken)
        ?? throw new StudentWithTokenNotFoundException(token);
}
