using EduConnect.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AttendanceConfigurations : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasIndex(a => new { a.StudentId, a.ClassId, a.Date }).IsUnique();

        builder.Property(a => a.Date)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(a => a.AttendanceStatus)
            .IsRequired();

        builder.HasOne(a => a.Student)
               .WithMany(s => s.Attendances)
               .HasForeignKey(a => a.StudentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Class)
               .WithMany(c => c.Attendances)
               .HasForeignKey(a => a.ClassId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
