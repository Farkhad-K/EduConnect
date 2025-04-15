using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions;

public interface IClassRepository
{
    ValueTask<IEnumerable<Class>> GetAllAsync(CancellationToken cancellationToken = default);
    ValueTask<Class?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<Class> CreateAsync(Class @class, CancellationToken cancellationToken = default);
    ValueTask<Class> UpdateAsync(Guid id, Class @class, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Class>> GetByAcademyIdAsync(Guid academyId, CancellationToken cancellationToken = default);
    // ValueTask<IEnumerable<Class>> GetByTeacherIdAsync(Guid academyId, Guid teacherId, CancellationToken cancellationToken = default);
}