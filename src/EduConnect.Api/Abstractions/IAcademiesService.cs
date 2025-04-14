using EduConnect.Api.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduConnect.Api.Abstractions;

public interface IAcademiesService
{
    Task<IEnumerable<Academy>> GetAllAcademiesAsync(CancellationToken cancellationToken = default);
    Task<Academy> GetAcademyByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Academy> GetAcademyByTokenAsync(string uniqueToken, CancellationToken cancellationToken = default);
    Task<Academy> CreateAcademyAsync(Academy academy, CancellationToken cancellationToken = default);
    Task<Academy> UpdateAcademyAsync(Guid id, Academy academy, CancellationToken cancellationToken = default);
    Task<bool> DeleteAcademyAsync(Guid id, CancellationToken cancellationToken = default);
}
