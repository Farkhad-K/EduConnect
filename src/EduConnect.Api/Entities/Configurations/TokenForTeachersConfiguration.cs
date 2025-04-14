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
            .WithMany(a => a.TeacherTokens)
            .HasForeignKey(t => t.AcademyId);
    }
}
