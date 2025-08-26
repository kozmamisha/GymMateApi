using GymMateApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMateApi.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
    {
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Text)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId);

            builder
                .HasOne(c => c.Training)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TrainingId);
        }
    }
}
