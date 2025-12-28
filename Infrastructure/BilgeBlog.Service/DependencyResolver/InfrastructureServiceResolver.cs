using BilgeBlog.Contract.Abstract;
using BilgeBlog.Service.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace BilgeBlog.Service.DependencyResolver
{
    public static class InfrastructureServiceResolver
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, JwtTokenService>();
        }
    }
}
