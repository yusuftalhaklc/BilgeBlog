using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Persistence.Context
{
    public class BilgeBlogDbContext : DbContext
    {
        public BilgeBlogDbContext(DbContextOptions<BilgeBlogDbContext> options) : base(options)
        {
        }

        public BilgeBlogDbContext()
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new PostCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PostTagConfiguration());
            modelBuilder.ApplyConfiguration(new PostLikeConfiguration());
        }
    }
}

