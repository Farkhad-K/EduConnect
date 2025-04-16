using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Entities;
using EduConnect.Api.Exceptions;

namespace EduConnect.Api.Services;

public class ClassesService(
    IClassRepository classRepository) : IClassesService
{
    public async ValueTask<IEnumerable<Class>> GetClassesByAcademyAsync(Guid academyId, CancellationToken cancellationToken = default)
    {
        return await classRepository.GetAllByAcademyIdAsync(academyId, cancellationToken);
    }

    public async ValueTask<IEnumerable<Class>> GetClassesByTeacherAsync(Guid academyId, Guid teacherId, CancellationToken cancellationToken = default)
        => (await classRepository.GetAllByAcademyIdAsync(academyId, cancellationToken)).Where(c => c.TeacherId == teacherId).ToList();

    // public async ValueTask<IEnumerable<Class>> GetAllClassesAsync(CancellationToken cancellationToken = default) =>
    //     await classRepository.GetAllAsync(cancellationToken);

    public async ValueTask<Class> GetClassByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await classRepository.GetByIdAsync(id, cancellationToken) ?? throw new ClassNotFoundException(id);

    public async ValueTask<Class> CreateClassAsync(Class @class, CancellationToken cancellationToken = default) =>
        await classRepository.CreateAsync(@class, cancellationToken);

    public async ValueTask<bool> DeleteClassAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleted = await classRepository.DeleteAsync(id, cancellationToken);
        if (!deleted)
            throw new ClassNotFoundException(id);

        return true;
    }

    public ValueTask<Class> UpdateClassAsync(Guid id, Class @class, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
