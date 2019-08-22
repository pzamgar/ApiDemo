using ApiBuildDemo.Infrastructure.Implementation;
using ApiBuildDemo.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBuildDemo.Infrastructure.Extensions {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddInfrastructureServices (this IServiceCollection services) {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IValueRepository, ValueRepository> ();
            services.AddScoped<IUserRepository,UserRepository>();
            return services;
        }
    }
}