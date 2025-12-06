using BilgeBlog.Contract.Abstract;
using BilgeBlog.Persistence.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace BilgeBlog.Persistence.DependencyResolvers
{
    public static class RepositoryResolver
    {
        public static void AddRepositoryService(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();
            services.AddScoped<IPostTagRepository, PostTagRepository>();
            services.AddScoped<IPostLikeRepository, PostLikeRepository>();
        }
    }
}

