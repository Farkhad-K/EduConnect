using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Abstractions.ServicesAbstractions;

namespace EduConnect.Api.Services;

public class TokensForTeachersService(
    ITokensForTeachersRepository tokenRepository) : ITokensForTeachersService
{
    public async ValueTask<string> GenerateTokenAsync(Guid adminAcademyId, CancellationToken cancellationToken = default)
    {
        var token = await tokenRepository.GenerateTokenAsync(adminAcademyId, cancellationToken);

        return token.Token!;
    }

    public async ValueTask<Guid?> ValidateTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var tokenEntity = await tokenRepository.GetTokenAsync(token, cancellationToken);
        return tokenEntity?.AcademyId;
    }
}