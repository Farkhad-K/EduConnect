using EduConnect.Api.Data;
using EduConnect.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Repositories;

public class UserRepository(IEduConnectDbContext context) : IUserRepository
{
    public async Task<UserBase?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var admin = await context.Admins.FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
        if (admin != null) return admin;

        var teacher = await context.Teachers.FirstOrDefaultAsync(t => t.Email == email, cancellationToken);
        if (teacher != null) return teacher;

        var parent = await context.Parents.FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
        if (parent != null) return parent;

        return null;
    }

    public async Task AddAsync(UserBase user, CancellationToken cancellationToken)
    {
        switch (user)
        {
            case Admin admin:
                await context.Admins.AddAsync(admin, cancellationToken);
                break;
            case Teacher teacher:
                await context.Teachers.AddAsync(teacher, cancellationToken);
                break;
            case Parent parent:
                await context.Parents.AddAsync(parent, cancellationToken);
                break;
            default:
                throw new ArgumentException("Unknown user type", nameof(user));
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
