using EduConnect.Api.Abstractions;
using EduConnect.Api.Entities;
using EduConnect.Api.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduConnect.Api.Services;

public class AcademiesService(
    IAcademyRepository academyRepository,
    ILogger<AcademiesService> logger) : IAcademiesService
{
    public async Task<IEnumerable<Academy>> GetAllAcademiesAsync(CancellationToken cancellationToken = default)
        => await academyRepository.GetAllAsync(cancellationToken);

    public async Task<Academy> GetAcademyByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await academyRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new AcademyNotFoundException(id);

    public async Task<Academy> GetAcademyByTokenAsync(string uniqueToken, CancellationToken cancellationToken = default)
        => await academyRepository.GetByTokenAsync(uniqueToken, cancellationToken)
            ?? throw new AcademyWithTokenNotFoundException(uniqueToken);

    public async Task<Academy> CreateAcademyAsync(Academy academy, CancellationToken cancellationToken = default)
        => await academyRepository.AddAsync(academy, cancellationToken);

    public async Task<Academy> UpdateAcademyAsync(Guid id, Academy updatedAcademy, CancellationToken cancellationToken = default)
    {
        var existingAcademy = await academyRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new AcademyNotFoundException(id);

        existingAcademy.Name = updatedAcademy.Name ?? existingAcademy.Name;
        existingAcademy.Address = updatedAcademy.Address ?? existingAcademy.Address;

        return await academyRepository.UpdateAsync(id, existingAcademy, cancellationToken);
    }

    public async Task<bool> DeleteAcademyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var success = await academyRepository.DeleteAsync(id, cancellationToken);
        if (!success)
        {
            logger.LogWarning("Failed to delete academy with id {Id}", id);
            throw new AcademyNotFoundException(id);
        }

        return true;
    }
}
