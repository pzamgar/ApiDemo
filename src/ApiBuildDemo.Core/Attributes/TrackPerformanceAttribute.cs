using System.Diagnostics;
using ApiBuildDemo.Core.Interfases;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ApiBuildDemo.Core.Attributes {
    public class TrackPerformanceAttribute : ActionFilterAttribute {
        private readonly ILoggerAdapter<TrackPerformanceAttribute> _logger;
        private readonly Stopwatch _timer;

        public TrackPerformanceAttribute (ILoggerAdapter<TrackPerformanceAttribute> logger) {
            _logger = logger;
            _timer = new Stopwatch ();
        }

        public override void OnActionExecuting (ActionExecutingContext context) {
            _timer.Start ();
        }

        public override void OnActionExecuted (ActionExecutedContext context) {
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