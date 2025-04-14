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