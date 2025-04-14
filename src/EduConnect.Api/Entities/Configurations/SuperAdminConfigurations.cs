using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class SuperAdminConfigurations : IEntityTypeConfiguration<SuperAdmin>
{
    public void Configure(EntityTypeBuilder<SuperAdmin> builder)
    {
        builder.HasKey(sa => sa.Id);
        builder.Property(sa => sa.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(sa => sa.Email)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(sa => sa.PasswordHash)
            .IsRequired()
            .HasColumnType("varchar(255)");
    }
}
