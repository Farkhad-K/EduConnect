using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions.ServicesAbstractions;

public interface IStudentsService
{
    ValueTask<IEnumerable<Student>> GetAllStudentsByAcademyIdAsync(Guid academyId, CancellationToken cancellationToken = default);
    ValueTask<Student> GetStudentByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<Student> GetStudentByTokenAsync(string uniqueToken, CancellationToken cancellationToken = default);
    ValueTask<Student> CreateStudentAsync(Student student, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteStudentAsync(Guid id, CancellationToken cancellationToken = default);
}