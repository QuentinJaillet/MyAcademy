using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyAcademy.Course.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Domain.Course>
{
    public void Configure(EntityTypeBuilder<Domain.Course> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
        builder.HasMany(c => c.Chapters).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}