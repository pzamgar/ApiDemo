using System;
using System.Net;
using System.Threading.Tasks;
using ApiBuildDemo.Api.Models;
using ApiBuildDemo.Core.Interfases;
using Microsoft.AspNetCore.Http;

namespace ApiBuildDemo.Api.Middleware {
    public class ExceptionMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILoggerAdapter<ExceptionMiddleware> _logger;

        public ExceptionMiddleware (RequestDelegate next, ILoggerAdapter<ExceptionMiddleware> logger) {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync (HttpContext httpContext) {
            try {
                await _next (httpContext);
            } catch (Exception ex) {
                _logger.LogError ($"Something went wrong: {ex}");
                await HandleExceptionAsync (httpContext, ex);
            }
        }

        private Task HandleExceptionAsync (HttpContext context, Exception exception) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync (new ErrorDetails () {
                StatusCode = context.Response.StatusCode,
                    Message = exception.Message
            }.ToString ());
        }
    }
}