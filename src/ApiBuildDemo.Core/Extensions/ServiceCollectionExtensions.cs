using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiBuildDemo.Core.Adapter;
using ApiBuildDemo.Core.Interfases;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBuildDemo.Core.Extensions {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddCoreServices (this IServiceCollection services) {
            services.AddSingleton (typeof (ILoggerAdapter<>), typeof (LoggerAdapter<>));
            return services;
        }
    }
}