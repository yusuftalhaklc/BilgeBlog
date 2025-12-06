using BilgeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Configuration
{
    public class CommentConfiguration : BaseEntityConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.ToTable("Comments");

            builder.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.PostId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.ParentCommentId)
                .IsRequired(false);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ParentComment)
                .WithMany(x => x.Replies)
                .HasForeignKey(x => x.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

