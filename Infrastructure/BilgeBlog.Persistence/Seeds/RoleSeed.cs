using BilgeBlog.Domain.Entities;
using BilgeBlog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeBlog.Persistence.Seeds
{
    public class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            var adminId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var authorId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000003");

            builder.HasData(
                new Role
                {
                    Id = adminId,
                    Name = RoleEnum.Admin.ToString(),
                    CreatedDate = DateTime.UtcNow
                },
                new Role
                {
                    Id = authorId,
                    Name = RoleEnum.Author.ToString(),
                    CreatedDate = DateTime.UtcNow
                },
                new Role
                {
                    Id = userId,
                    Name = RoleEnum.User.ToString(),
                    CreatedDate = DateTime.UtcNow
                }
            );
        }
    }
}

