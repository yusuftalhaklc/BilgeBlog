using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class PostLikeConfiguration : BaseEntityConfiguration<PostLike>
    {
        public override void Configure(EntityTypeBuilder<PostLike> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.Id);
            builder.ToTable("PostLikes");

            builder.HasKey(x => new { x.PostId, x.UserId });

            builder.Property(x => x.PostId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

