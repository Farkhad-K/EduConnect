using EduConnect.Api.Abstractions;
using EduConnect.Api.Entities;
using EduConnect.Api.Exceptions;

namespace EduConnect.Api.Services;

public class StudentsService(
    IStudentsRepository repository,
    ILogger<StudentsService> logger) : IStudentsService
{
    public async ValueTask<Student> CreateStudentAsync(Student student, CancellationToken cancellationToken = default)
        => await repository.AddAsync(student, cancellationToken);

    public async ValueTask<bool> DeleteStudentAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var success = await repository.DeleteAsync(id, cancellationToken);
        if (!success)
        {
            logger.LogWarning("Failed to delete student with id {Id}", id);
            throw new StudentNotFoundException(id);
        }

        return true;
    }

    public async ValueTask<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default)
        => await repository.GetAllAsync(cancellationToken);

    public async ValueTask<Student> GetStudenByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await repository.GetByIdAsync(id, cancellationToken)
        ?? throw new StudentNotFoundException(id);

    public async ValueTask<Student> GetStudenByTokenAsync(string uniqueToken, CancellationToken cancellationToken = default)
        => await repository.GetByTokenAsync(uniqueToken, cancellationToken)
        ?? throw new StudentWithTokenNotFoundException(uniqueToken);
}