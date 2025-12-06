using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class PostCategoryConfiguration : BaseEntityConfiguration<PostCategory>
    {
        public override void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.Id);
            builder.ToTable("PostCategories");

            builder.HasKey(x => new { x.PostId, x.CategoryId });

            builder.Property(x => x.PostId)
                .IsRequired();

            builder.Property(x => x.CategoryId)
                .IsRequired();

            builder.HasOne(x => x.Post)
                .WithMany(x => x.PostCategories)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.PostCategories)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

