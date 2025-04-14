using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class TeacherConfigurations : UserBaseConfigurations<Teacher>
{
    public override void Configure(EntityTypeBuilder<Teacher> builder)
    {
        base.Configure(builder);

        builder.HasOne(t => t.Academy)
            .WithMany(a => a.Teachers)
            .HasForeignKey(t => t.AcademyId);

        builder.HasMany(t => t.Classes)
            .WithOne(c => c.Teacher)
            .HasForeignKey(c => c.TeacherId);
    }
}
