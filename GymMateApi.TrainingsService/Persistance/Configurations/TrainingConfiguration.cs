using GymMateApi.TrainingsService.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMateApi.TrainingsService.Persistance.Configurations;

public class TrainingConfiguration : IEntityTypeConfiguration<TrainingEntity>
{
    public void Configure(EntityTypeBuilder<TrainingEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(500)
            .IsRequired();
    }
}
