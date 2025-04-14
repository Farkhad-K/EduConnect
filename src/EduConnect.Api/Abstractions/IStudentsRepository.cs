using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions;

public interface IStudentsRepository
{
    ValueTask<Student> AddAsync(Student student, CancellationToken cancellationToken = default);
    ValueTask<Student?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    ValueTask<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default);
}