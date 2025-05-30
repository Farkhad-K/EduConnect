namespace EduConnect.Api.Abstractions.ServicesAbstractions;

public interface ITokensForTeachersService
{
    ValueTask<string> GenerateTokenAsync(Guid adminAcademyId, CancellationToken cancellationToken = default);
    ValueTask<Guid?> ValidateTokenAsync(string token, CancellationToken cancellationToken = default);
}