using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class ParentConfigurations : UserBaseConfigurations<Parent>
{
    public override void Configure(EntityTypeBuilder<Parent> builder)
    {
        base.Configure(builder);

        builder.HasMany(p => p.Students)
            .WithMany(s => s.Parents)
            .UsingEntity(j => j.ToTable("ParentStudents"));
    }
}
