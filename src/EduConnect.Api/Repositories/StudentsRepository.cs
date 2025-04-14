using EduConnect.Api.Abstractions;
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

    public async ValueTask<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Students.AsNoTracking().ToListAsync(cancellationToken);

    public async ValueTask<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Students.FirstOrDefaultAsync(s => s.Id == id, cancellationToken)
        ?? throw new StudentNotFoundException(id);

    public async ValueTask<Student?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        => await context.Students.FirstOrDefaultAsync(s => s.UniqueToken == token, cancellationToken)
        ?? throw new StudentWithTokenNotFoundException(token);
}
