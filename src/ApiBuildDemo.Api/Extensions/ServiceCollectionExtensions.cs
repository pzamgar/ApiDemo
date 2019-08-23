using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ApiBuildDemo.Core.Options;
using ApiBuildDemo.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Microsoft.Extensions.DependencyInjection {
    public static class ServiceCollectionExtensions {

        #region Extension Methods
        public static IServiceCollection AddContextCustom (this IServiceCollection services,
            IConfiguration configuration) {

            var connectionString = configuration.GetConnectionString ("ValueConnection");
            services.AddDbContext<ValueContext> (o => o.UseSqlServer (connectionString));
            services.AddDbContext<RepositoryContext> (o => o.UseSqlServer (connectionString));

            return services;
        }

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

                options.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement (new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new List<string> { "readAccess", "writeAccess" }
                    }
                });
            });
            return services;
        }

        public static IServiceCollection ConfigurationJwtAuthorization (this IServiceCollection services,
            IConfiguration configuration) {

            var authSettingsSection = configuration.GetSection ("AuthSettings");
            services.Configure<AuthSettings> (authSettingsSection);

            var authSettings = authSettingsSection.Get<AuthSettings> ();
            var key = Encoding.ASCII.GetBytes (authSettings.Secret);

            services.AddAuthentication (x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey (key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }
        #endregion

        #region Private Methods
        private static string XmlCommentsFilePath () {

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
            return xmlPath;
        }
        #endregion
    }
}