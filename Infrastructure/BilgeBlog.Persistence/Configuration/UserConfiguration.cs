using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.ToTable("Users");

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.RoleId)
                .IsRequired();

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

