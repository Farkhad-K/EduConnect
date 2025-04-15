using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions;

public interface ITeachersService
{
    ValueTask<IEnumerable<Teacher>> GetAllTeachersAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<Teacher> GetTeacherByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteTeacherAsync(Guid id, CancellationToken cancellationToken = default);
}