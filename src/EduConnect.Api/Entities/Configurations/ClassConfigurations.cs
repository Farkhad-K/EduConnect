using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class ClassConfigurations : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Classes");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => new { c.Name, c.AcademyId }).IsUnique();
        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(c => c.StartTime)
                   .IsRequired();
        builder.Property(c => c.EndTime)
               .IsRequired();
        builder.Property(c => c.Schedule)
                   .HasConversion<int>()
                   .IsRequired();

        builder.HasOne(c => c.Academy)
                   .WithMany(a => a.Classes)
                   .HasForeignKey(c => c.AcademyId);
    }
}