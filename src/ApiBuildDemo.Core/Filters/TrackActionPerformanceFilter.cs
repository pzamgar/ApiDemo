using System;
using System.Diagnostics;
using ApiBuildDemo.Core.Interfases;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiBuildDemo.Core.Filters {
    public class TrackActionPerformanceFilter : IActionFilter {
        private Stopwatch _timer;
        private readonly ILoggerAdapter<TrackActionPerformanceFilter> _logger;

        public TrackActionPerformanceFilter (ILoggerAdapter<TrackActionPerformanceFilter> logger) {
            _logger = logger;
        }
        public void OnActionExecuting (ActionExecutingContext context) {
            _timer = new Stopwatch ();
            _timer.Start ();
        }

        public void OnActionExecuted (ActionExecutedContext context) {
            _timer.Stop ();
            if (context.Exception == null) {
                var RouteName = context.HttpContext.Request.Path;
                var Name = context.HttpContext.Request.Method;
                var ElapsedMilliseconds = _timer.ElapsedMilliseconds;
                _logger.LogInformation ($"TrackActionPerformanceFilter - {RouteName} {Name} code took {ElapsedMilliseconds}");
            }
        }
    }
}