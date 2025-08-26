using GymMateApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMateApi.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
    {
        public void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasMany(c => c.Subscribers)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);

            builder
                .HasMany(c => c.Trainings)
                .WithMany(t => t.Courses);
        }
    }
}
