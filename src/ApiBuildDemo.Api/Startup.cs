using ApiBuildDemo.Core.Extensions;
using ApiBuildDemo.Core.Filters;
using ApiBuildDemo.Infrastructure.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiBuildDemo.Api {
    public class Startup {
        public Startup (IConfiguration configuration, IWebHostEnvironment currentEnvironment) {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices (IServiceCollection services) {
            services.AddContextCustom (Configuration, CurrentEnvironment);

            services.AddControllers (options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add (typeof (TrackActionPerformanceFilter));
            }).SetCompatibilityVersion (CompatibilityVersion.Latest);

            services.AddApiVersionCustom ();
            services.AddSwaggerCustom ();
            services.AddLogging (loggingBuilder => {
                loggingBuilder.AddSeq ();
            });

            services.AddHealthChecksCustom (Configuration, CurrentEnvironment);

            // JWT Configuration
            services.ConfigurationJwtAuthorization (Configuration);

            services.AddCoreServices ();
            services.AddInfrastructureServices ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider) {
            if (env.IsDevelopment()) {
                app.UpdateDatabase ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            //app.ConfigureExceptionHandler (); 
            app.ConfigureCustomExceptionMiddleware ();

            app.UseSwagger ();
            app.UseSwaggerUI (c => {

                foreach (var description in provider.ApiVersionDescriptions) {
                    c.SwaggerEndpoint ($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant ());
                    c.RoutePrefix = string.Empty;
                }
            });

            app.UseHealthChecks ("/hc", new HealthCheckOptions () {
                Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI ();

            app.UseCors (o => o
                .AllowAnyOrigin ()
                .AllowAnyMethod ()
                .AllowAnyHeader ());

            app.UseHttpsRedirection ();
            app.UseRouting();
            app.UseAuthentication ();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}