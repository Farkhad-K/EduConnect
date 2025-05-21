using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions.RepositoriesAbstractions;

public interface IParentsRepository
{
    ValueTask<bool> AttachToParent(Guid parentId, Guid studentId, CancellationToken cancellationToken = default); 
    ValueTask<bool> ReomveFromParent(Guid parentId, Guid studentId, CancellationToken cancellationToken = default); 
    ValueTask<Parent> GetChildren(Guid parentId, CancellationToken cancellationToken = default); 
}