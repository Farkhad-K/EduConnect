using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Data;
using EduConnect.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Repositories;

public class ClassRepository(IEduConnectDbContext context) : IClassRepository
{
    public async ValueTask<Class> CreateAsync(Class @class, CancellationToken cancellationToken = default)
    {
        var entry = context.Classes.Add(@class);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async ValueTask<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingClass = await GetByIdAsync(id, cancellationToken);
        if (existingClass == null)
            return false;

        context.Classes.Remove(existingClass);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async ValueTask<IEnumerable<Class>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Classes.AsNoTracking().ToListAsync(cancellationToken);

    public async ValueTask<IEnumerable<Class>> GetAllByAcademyIdAsync(Guid academyId, CancellationToken cancellationToken = default)
    {
        return await context.Classes
            .Where(c => c.AcademyId == academyId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async ValueTask<Class?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Classes.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public ValueTask<Class> UpdateAsync(Guid id, Class @class, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}