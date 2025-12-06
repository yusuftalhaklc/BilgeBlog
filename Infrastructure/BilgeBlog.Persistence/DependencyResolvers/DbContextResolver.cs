using BilgeBlog.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BilgeBlog.Persistence.DependencyResolvers
{
    public static class DbContextResolver
    {
        public static void AddDbContextService(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            IConfiguration configuration = provider.GetRequiredService<IConfiguration>();

            services.AddDbContext<BilgeBlogDbContext>(opt => 
                opt.UseSqlServer(configuration.GetConnectionString("BilgeBlogConnection"))
                   .UseLazyLoadingProxies());
        }
    }
}

