using EduConnect.Api.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduConnect.Api.Abstractions.RepositoriesAbstractions;

public interface IAcademyRepository
{
    Task<IEnumerable<Academy>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Academy?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Academy?> GetByTokenAsync(string uniqueToken, CancellationToken cancellationToken = default);
    Task<Academy> AddAsync(Academy academy, CancellationToken cancellationToken = default);
    Task<Academy> UpdateAsync(Guid id, Academy academy, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
