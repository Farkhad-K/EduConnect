using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Entities;


namespace EduConnect.Api.Services;

public class ParentsService(
    IParentsRepository parentsRepository) : IParentsService
{
    public async ValueTask<bool> AttachStudentToParent(Guid parentId, Guid studentId, CancellationToken cancellationToken = default)
        => await parentsRepository.AttachToParent(parentId, studentId, cancellationToken);

    public async ValueTask<bool> RemoveStudentFromParent(Guid parentId, Guid studentId, CancellationToken cancellationToken = default)
        => await parentsRepository.ReomveFromParent(parentId, studentId, cancellationToken);

    public async ValueTask<Parent> GetChildrenOfParent(Guid parentId, CancellationToken cancellationToken = default)
        => await parentsRepository.GetChildren(parentId, cancellationToken);
}
