using System.Net;
using ApiBuildDemo.Api.Middleware;
using ApiBuildDemo.Api.Models;
using ApiBuildDemo.Core.Interfases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

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
    }
}