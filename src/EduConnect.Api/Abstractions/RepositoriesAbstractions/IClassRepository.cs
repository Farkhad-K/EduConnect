using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions.RepositoriesAbstractions;

public interface IClassRepository
{
    ValueTask<Class?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<Class> CreateAsync(Class @class, CancellationToken cancellationToken = default);
    ValueTask<Class> UpdateAsync(Guid id, Class @class, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Class>> GetAllByAcademyIdAsync(Guid academyId, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Class>> GetByTeacherIdAsync(Guid academyId, Guid teacherId, CancellationToken cancellationToken = default);
}