using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions.RepositoriesAbstractions;

public interface IUserRepository
{
    Task<UserBase?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AddAsync(UserBase user, CancellationToken cancellationToken = default);
}