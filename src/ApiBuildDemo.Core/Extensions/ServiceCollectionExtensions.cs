using ApiBuildDemo.Core.Adapter;
using ApiBuildDemo.Core.Interfases;
using ApiBuildDemo.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBuildDemo.Core.Extensions {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddCoreServices (this IServiceCollection services) {
            services.AddSingleton (typeof (ILoggerAdapter<>), typeof (LoggerAdapter<>));
            services.AddTransient<IValueService, ValueService> ();
            services.AddTransient<IUserService, UserService> ();
            services.AddTransient<IAuthService, AuthService> ();
            return services;
        }
    }
}