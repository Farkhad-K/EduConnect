using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions.RepositoriesAbstractions;

public interface ITeacherRepository
{
    ValueTask<IEnumerable<Teacher>> GetAllAsync(Guid academyId, CancellationToken cancellationToken = default);
    ValueTask<Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}