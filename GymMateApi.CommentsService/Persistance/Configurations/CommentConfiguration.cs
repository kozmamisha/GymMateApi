using GymMateApi.CommentsService.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMateApi.CommentsService.Persistance.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Text)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.AuthorId)
            .IsRequired();

        builder.Property(c => c.TrainingId)
            .IsRequired();

        builder.HasIndex(c => c.AuthorId);

        builder.HasIndex(c => c.TrainingId);
    }
}
