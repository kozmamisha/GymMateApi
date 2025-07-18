using GymMateApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistance.Configurations
{
    public class TrainingConfiguration : IEntityTypeConfiguration<TrainingEntity>
    {
        public void Configure(EntityTypeBuilder<TrainingEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasMany(e => e.Comments)
                .WithOne(u => u.Training)
                .HasForeignKey(e => e.TrainingId);

            builder.HasMany(e => e.Exercises)
                .WithOne(u => u.Training)
                .HasForeignKey(e => e.TrainingId);

            builder.HasMany(e => e.Courses)
                .WithMany(u => u.Trainings);
        }
    }
}
