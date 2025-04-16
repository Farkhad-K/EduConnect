using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions;

public interface IClassesService
{
    // ValueTask<IEnumerable<Class>> GetAllClassesAsync(CancellationToken cancellationToken = default);
    ValueTask<Class> GetClassByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<Class> CreateClassAsync(Class @class, CancellationToken cancellationToken = default);
    ValueTask<Class> UpdateClassAsync(Guid id, Class @class, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteClassAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Class>> GetClassesByAcademyAsync(Guid academyId, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Class>> GetClassesByTeacherAsync(Guid academyId, Guid teacherId, CancellationToken cancellationToken = default);
}