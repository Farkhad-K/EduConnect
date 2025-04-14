using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class AdminConfigurations : UserBaseConfigurations<Admin>
{
    public override void Configure(EntityTypeBuilder<Admin> builder)
    {
        base.Configure(builder);

         builder.HasOne(a => a.Academy)
            .WithMany(a => a.Admins)
            .HasForeignKey(a => a.AcademyId);
    }
}