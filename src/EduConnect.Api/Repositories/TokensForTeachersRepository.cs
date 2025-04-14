using EduConnect.Api.Abstractions;
using EduConnect.Api.Data;
using EduConnect.Api.Entities;
using EduConnect.Api.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Repositories;

public class TokensForTeachersRepository(
    IEduConnectDbContext context) : ITokensForTeachersRepository
{
    public async ValueTask<IEnumerable<TokenForTeachers>> GetTokenForTeachersAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var tokensOfAcademy = await context.TokensForTeachers
            .Where(a => a.AcademyId == id)
            .ToListAsync(cancellationToken);

        return tokensOfAcademy;
    }

    public async ValueTask<bool> DeleteTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var existingToken = await context.TokensForTeachers
            .FirstOrDefaultAsync(t => t.Token == token, cancellationToken);
        if (existingToken == null)
            return false;

        context.TokensForTeachers.Remove(existingToken);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async ValueTask<TokenForTeachers> GenerateTokenAsync(Guid academyId, CancellationToken cancellationToken = default)
    {
        var newToken = new TokenForTeachers
        {
            Id = new Guid(),
            Token = TokenGenerator.GenerateToken(),
            AcademyId = academyId
        };

        context.TokensForTeachers.Add(newToken);
        await context.SaveChangesAsync(cancellationToken);
        return newToken;
    }

    public async ValueTask<TokenForTeachers?> GetTokenAsync(string token, CancellationToken cancellationToken = default)
        => await context.TokensForTeachers.FirstOrDefaultAsync(t => t.Token == token, cancellationToken);
}