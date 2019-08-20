using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ApiBuildDemo.Core.Extensions;
using ApiBuildDemo.Core.Filters;
using ApiBuildDemo.Infrastructure.Data;
using HealthChecks.Publisher.Seq;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiBuildDemo.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddContextCustom (Configuration);

            services.AddMvc (options => {
                options.Filters.Add (typeof (TrackActionPerformanceFilter));
            }).SetCompatibilityVersion (CompatibilityVersion.Version_2_2);

            services.AddApiVersionCustom ();
            services.AddSwaggerCustom ();
            services.AddLogging (loggingBuilder => {
                loggingBuilder.AddSeq ();
            });

            var seqServerUrl = Configuration["Serilog:SeqServerUrl"];
            services.AddHealthChecks ()
                .AddCheck ("unhealthy", check => HealthCheckResult.Unhealthy ())
                .AddSeqPublisher (options => {
                    options.Endpoint = string.IsNullOrWhiteSpace (seqServerUrl) ? "http://seq" : seqServerUrl;
                    options.ApiKey = "A8buUymer3O9Iq0mc2G7";
                });
            services.AddHealthChecksUI ();

            services.AddCoreServices ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app,
            IHostingEnvironment env,
            IApiVersionDescriptionProvider provider) {
            if (!env.IsDevelopment ()) {
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
            app.UseHttpsRedirection ();
            app.UseMvc ();
        }
    }
}