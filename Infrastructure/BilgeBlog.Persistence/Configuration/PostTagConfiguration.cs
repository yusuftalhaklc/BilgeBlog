using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class PostTagConfiguration : BaseEntityConfiguration<PostTag>
    {
        public override void Configure(EntityTypeBuilder<PostTag> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.Id);
            builder.ToTable("PostTags");

            builder.HasKey(x => new { x.PostId, x.TagId });

            builder.Property(x => x.PostId)
                .IsRequired();

            builder.Property(x => x.TagId)
                .IsRequired();

            builder.HasOne(x => x.Post)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

