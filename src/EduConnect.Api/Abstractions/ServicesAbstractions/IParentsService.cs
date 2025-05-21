using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions.ServicesAbstractions;

public interface IParentsService
{
    ValueTask<bool> AttachStudentToParent(Guid parentId, Guid studentId, CancellationToken cancellationToken = default);
    ValueTask<bool> RemoveStudentFromParent(Guid parentId, Guid studentId, CancellationToken cancellationToken = default);
    ValueTask<Parent> GetChildrenOfParent(Guid parentId, CancellationToken cancellationToken = default);
}