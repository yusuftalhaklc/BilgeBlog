using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedDate)
                .IsRequired(false);

            builder.Property(x => x.DeletedDate)
                .IsRequired(false);

            builder.HasQueryFilter(x => x.DeletedDate == null);
        }
    }
}

