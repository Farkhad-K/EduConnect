using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Data;
using EduConnect.Api.Entities;
using EduConnect.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Repositories;

public class AcademyRepository(
    IEduConnectDbContext context,
    ILogger<AcademyRepository> logger) : IAcademyRepository
{
    public async Task<Academy> AddAsync(Academy academy, CancellationToken cancellationToken = default)
    {
        var entry = context.Academies.Add(academy);

        await context.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Academies.Remove(new Academy { Id = id });
            return await context.SaveChangesAsync(cancellationToken) is 1;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to delete academy with id {Id}", id);
            throw new AcademyNotFoundException(id);
        }
    }

    public async Task<IEnumerable<Academy>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Academies.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Academy?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Academies.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
        ?? throw new AcademyNotFoundException(id);

    public async Task<Academy?> GetByTokenAsync(string uniqueToken, CancellationToken cancellationToken = default)
        => await context.Academies.FirstOrDefaultAsync(a => a.UniqueToken == uniqueToken, cancellationToken)
            ?? throw new AcademyWithTokenNotFoundException(uniqueToken);

    public async Task<Academy> UpdateAsync(Guid id, Academy academy, CancellationToken cancellationToken = default)
    {
        var existingAcademy = await context.Academies.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
                              ?? throw new AcademyNotFoundException(id);

        try
        {
            existingAcademy.Name = academy.Name ?? existingAcademy.Name;
            existingAcademy.Address = academy.Address ?? existingAcademy.Address;

            await context.SaveChangesAsync(cancellationToken);
            return existingAcademy;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to update academy with id {Id}.", id);
            throw new AcademyNotFoundException(id);
        }
    }
}