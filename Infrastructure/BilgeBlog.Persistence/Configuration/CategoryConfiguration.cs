using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.ToTable("Categories");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasMany(x => x.PostCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

