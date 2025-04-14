using EduConnect.Api.Entities;

namespace EduConnect.Api.Abstractions;

public interface ITokensForTeachersRepository
{
    ValueTask<TokenForTeachers> GenerateTokenAsync(Guid academyId, CancellationToken cancellationToken = default);
    ValueTask<TokenForTeachers?> GetTokenAsync(string token, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteTokenAsync(string token, CancellationToken cancellationToken = default);
}