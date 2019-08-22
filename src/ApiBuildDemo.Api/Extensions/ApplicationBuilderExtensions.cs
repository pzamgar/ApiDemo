using System.Net;
using ApiBuildDemo.Api.Middleware;
using ApiBuildDemo.Api.Models;
using ApiBuildDemo.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection {
    public static class ApplicationBuilderExtensions {
        public static void ConfigureExceptionHandler (this IApplicationBuilder app) {
            app.UseExceptionHandler (appError => {
                appError.Run (async context => {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature> ();
                    if (contextFeature != null) {
                        //logger.LogError ($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync (new ErrorDetails () {
                            StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error."
                        }.ToString ());
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware (this IApplicationBuilder app) {
            app.UseMiddleware<ExceptionMiddleware> ();
        }

        public static void UpdateDatabase (this IApplicationBuilder app) {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory> ().CreateScope ()) {
                using (var context = serviceScope.ServiceProvider.GetService<RepositoryContext> ()) {
                    if (!context.Database.EnsureCreated ())
                        context.Database.Migrate ();
                }
            }
        }
    }
}