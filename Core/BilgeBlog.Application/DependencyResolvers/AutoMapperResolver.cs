using BilgeBlog.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace BilgeBlog.Application.DependencyResolvers
{
    public static class AutoMapperResolver
    {
        public static void AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}

