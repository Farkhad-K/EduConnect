using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions.ServicesAbstractions;

public interface IStudentsService
{
    ValueTask<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
    ValueTask<Student> GetStudenByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<Student> GetStudenByTokenAsync(string uniqueToken, CancellationToken cancellationToken = default);
    ValueTask<Student> CreateStudentAsync(Student student, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteStudentAsync(Guid id, CancellationToken cancellationToken = default);
}