using BilgeBlog.WebApi.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace BilgeBlog.WebApi.DependencyResolvers
{
    public static class WebApiAutoMapperResolver
    {
        public static void AddWebApiAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RequestMappingProfile));
        }
    }
}

