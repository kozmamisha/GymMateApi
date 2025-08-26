using GymMateApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMateApi.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(a => a.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Email)
                .IsRequired();

            builder.Property(a => a.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .HasMany(u => u.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);

            builder
                .HasOne(u => u.Course)
                .WithMany(c => c.Subscribers)
                .HasForeignKey(u => u.CourseId)
                .IsRequired(false);
        }
    }
}
