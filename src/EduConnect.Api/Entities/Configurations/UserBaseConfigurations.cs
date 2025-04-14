using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public abstract class UserBaseConfigurations<T> : IEntityTypeConfiguration<T> where T : UserBase
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(u => u.Email)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(u => u.Role)
                   .HasConversion<int>()
                   .IsRequired();
        builder.Property(u => u.PasswordHash)
        .IsRequired()
        .HasColumnType("varchar(255)");
    }
}