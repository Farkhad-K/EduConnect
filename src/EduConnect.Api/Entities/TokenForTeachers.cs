namespace EduConnect.Api.Entities;

public class TokenForTeachers
{
    public Guid Id { get; set; }
    public string? Token { get; set; }
    public Guid AcademyId { get; set; }
    public Academy Academy { get; set; } = null!;
}

/*
warn: Microsoft.EntityFrameworkCore.Model.Validation[10625]
      The foreign key property 'TokenForTeachers.AcademyId1' was created in shadow state because a conflicting property with the simple name 'AcademyId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class AcademyConfigurations : IEntityTypeConfiguration<Academy>
{
    public void Configure(EntityTypeBuilder<Academy> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(a => a.Address)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.HasIndex(a => a.UniqueToken).IsUnique();
        builder.Property(a => a.UniqueToken)
               .IsRequired()
               .HasMaxLength(6);

        // Realationships
        builder.HasMany(a => a.Teachers)
               .WithOne(t => t.Academy)
               .HasForeignKey(t => t.AcademyId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Students)
               .WithOne(s => s.Academy)
               .HasForeignKey(s => s.AcademyId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Classes)
               .WithOne(c => c.Academy)
               .HasForeignKey(c => c.AcademyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class TokenForTeachersConfiguration : IEntityTypeConfiguration<TokenForTeachers>
{
    public void Configure(EntityTypeBuilder<TokenForTeachers> builder)
    {
        builder.ToTable("TokenForTeachers");
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.Token).IsUnique();
        builder.Property(t => t.Token)
            .HasColumnType("varchar(6)");

        builder.HasOne(t => t.Academy)
            .WithMany()
            .HasForeignKey(t => t.AcademyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

namespace EduConnect.Api.Entities;

public class TokenForTeachers
{
    public Guid Id { get; set; }
    public string? Token { get; set; }
    public Guid? AcademyId { get; set; }
    public Academy? Academy { get; set; }
}
using EduConnect.Api.Utilities;

namespace EduConnect.Api.Entities;

public class Teacher : UserBase
{
    public Guid? AcademyId { get; set; }
    public Academy? Academy { get; set; }
    public ICollection<Class> Classes { get; set; } = new List<Class>();
}

using EduConnect.Api.Utilities;

namespace EduConnect.Api.Entities;

public class Academy
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? UniqueToken { get; set; }

    public ICollection<Admin> Admins { get; set; } = new List<Admin>();
    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Class> Classes { get; set; } = new List<Class>();
    public ICollection<TokenForTeachers> TeacherTokens { get; set; } = new List<TokenForTeachers>();
}

*/