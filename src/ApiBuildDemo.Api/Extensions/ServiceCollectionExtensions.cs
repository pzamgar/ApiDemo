using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection {
    public static class ServiceCollectionExtensions {

        public static IServiceCollection AddApiVersionCustom (this IServiceCollection services) {

            services.AddApiVersioning (options => {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion (1, 0);
            });

            services.AddVersionedApiExplorer (
                options => {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }

        public static IServiceCollection AddSwaggerCustom (this IServiceCollection services) {

            services.AddSwaggerGen (options => {

                var provider = services.BuildServiceProvider ()
                    .GetRequiredService<IApiVersionDescriptionProvider> ();

                options.DescribeAllParametersInCamelCase ();
                options.DescribeStringEnumsInCamelCase ();
                foreach (var apiVersion in provider.ApiVersionDescriptions) {
                    var apiVersionName = apiVersion.ApiVersion.ToString ();
                    options.SwaggerDoc (apiVersion.GroupName,
                        new OpenApiInfo {
                            Title = "Demo Api rest Notes",
                                Version = apiVersionName,
                                Contact = new OpenApiContact {
                                    Email = "pzamgar@gmail.com",
                                        Name = "pzamgar",
                                        Url = new Uri ("https://github.com/pzamgar")
                                },
                                Description = $"A simple example ASP.NET Core Web API Notes {apiVersionName}",
                                License = new OpenApiLicense {
                                    Name = "MIT",
                                        Url = new Uri ("https://opensource.org/licenses/MIT")
                                }
                        }
                    );
                }

                options.IncludeXmlComments (XmlCommentsFilePath ());
            });
            return services;
        }

        private static string XmlCommentsFilePath () {

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
            return xmlPath;
        }
    }
}