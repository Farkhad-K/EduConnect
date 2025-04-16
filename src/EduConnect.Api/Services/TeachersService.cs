using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Entities;
using EduConnect.Api.Exceptions;

namespace EduConnect.Api.Services;

public class TeachersService(
    ITeacherRepository teacherRepository) : ITeachersService
{
    public async ValueTask<Teacher> GetTeacherByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await teacherRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new TeacherNotFoundException(id);

    public async ValueTask<IEnumerable<Teacher>> GetAllTeachersAsync(Guid academyId,CancellationToken cancellationToken = default)
        => await teacherRepository.GetAllAsync(academyId, cancellationToken);

    public async ValueTask<bool> DeleteTeacherAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleted = await teacherRepository.DeleteAsync(id, cancellationToken);
        if (!deleted)
            throw new TeacherNotFoundException(id);

        return true;
    }
}