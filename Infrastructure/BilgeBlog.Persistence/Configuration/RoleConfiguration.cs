using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class RoleConfiguration : BaseEntityConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.ToTable("Roles");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasMany(x => x.Users)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

