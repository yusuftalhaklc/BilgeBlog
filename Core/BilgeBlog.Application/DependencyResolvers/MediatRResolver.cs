using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BilgeBlog.Application.DependencyResolvers
{
    public static class MediatRResolver
    {
        public static void AddMediatRService(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}

