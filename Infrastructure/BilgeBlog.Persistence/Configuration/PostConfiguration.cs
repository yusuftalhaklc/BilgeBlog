using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class PostConfiguration : BaseEntityConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.ToTable("Posts");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(x => x.Slug)
                .IsUnique();

            builder.Property(x => x.Content)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.IsPublished)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PostCategories)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PostTags)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

