using GymMateApi.CoursesService.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMateApi.CoursesService.Persistance.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Ratings)
            .IsRequired();

        builder.Property(c => c.SubscriberIds)
            .IsRequired();

        builder.Property(c => c.TrainingIds)
            .IsRequired();
    }
}
