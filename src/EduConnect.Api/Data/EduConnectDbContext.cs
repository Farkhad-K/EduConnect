using EduConnect.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Data;

public class EduConnectDbContext(
    DbContextOptions<EduConnectDbContext> options) 
    : DbContext(options), IEduConnectDbContext
{
    public DbSet<Academy> Academies { get; set; } = default!;
    public DbSet<Admin> Admins { get; set; } = default!;
    public DbSet<Class> Classes { get; set; } = default!;
    public DbSet<Parent> Parents { get; set; } = default!;
    public DbSet<Student> Students { get; set; } = default!;
    public DbSet<Teacher> Teachers { get; set; } = default!;
    public DbSet<SuperAdmin> SuperAdmins { get; set; } = default!;
    public DbSet<TokenForTeachers> TokensForTeachers { get; set; } = default!;
    public DbSet<Attendance> Attendances { get; set; } = default!;

    async ValueTask<int> IEduConnectDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        => await base.SaveChangesAsync(cancellationToken);
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EduConnectDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}