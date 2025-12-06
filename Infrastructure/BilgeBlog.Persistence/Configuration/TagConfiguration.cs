using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class TagConfiguration : BaseEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.ToTable("Tags");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasMany(x => x.PostTags)
                .WithOne(x => x.Tag)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

