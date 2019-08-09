using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection {
    public static class ServiceCollectionExtensions {

        public static IServiceCollection AddSwaggerCustom (this IServiceCollection services) {

            services.AddSwaggerGen (c => {
                c.DescribeAllParametersInCamelCase ();
                c.DescribeStringEnumsInCamelCase ();
                c.SwaggerDoc ("v1",
                    new OpenApiInfo {
                        Title = "Customer API",
                            Version = "v1",
                            Contact = new OpenApiContact {
                                Email = "pzamgar@gmail.com",
                                    Name = "pzamgar",
                                    Url = new Uri("https://github.com/pzamgar")
                            },
                            Description = "A simple example ASP.NET Core Web API",
                            License = new OpenApiLicense {
                                Name = "MIT",
                                    Url = new Uri("https://opensource.org/licenses/MIT")
                            }
                    }
                );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments (xmlPath);
            });
            return services;
        }
    }
}