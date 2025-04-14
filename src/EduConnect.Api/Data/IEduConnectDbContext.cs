using EduConnect.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EduConnect.Api.Data;

public interface IEduConnectDbContext
{
    DatabaseFacade Database { get; }
    DbSet<Academy> Academies { get; set; }
    DbSet<Admin> Admins { get; set; }
    DbSet<Class> Classes { get; set; }
    DbSet<Parent> Parents { get; set; }
    DbSet<Student> Students { get; set; }
    DbSet<Teacher> Teachers { get; set; }
    DbSet<SuperAdmin> SuperAdmins { get; set; }
    DbSet<TokenForTeachers> TokensForTeachers { get; set; }
    ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}