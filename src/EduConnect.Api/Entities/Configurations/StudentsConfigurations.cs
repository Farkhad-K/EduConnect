using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class StudentConfigurations : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name)
            .HasColumnType("varchar(50)")
            .IsRequired();
        builder.HasIndex(s => s.UniqueToken)
            .IsUnique();
        builder.Property(s => s.UniqueToken)
               .IsRequired()
               .HasMaxLength(6);

        builder.HasOne(s => s.Academy)
               .WithMany(a => a.Students)
               .HasForeignKey(s => s.AcademyId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(s => s.Classes)
            .WithMany(c => c.Students)
            .UsingEntity(j => j.ToTable("StudentClasses"));
    }
}