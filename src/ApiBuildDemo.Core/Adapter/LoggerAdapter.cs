using ApiBuildDemo.Core.Interfases;
using Microsoft.Extensions.Logging;

namespace ApiBuildDemo.Core.Adapter {
    public class LoggerAdapter<T> : ILoggerAdapter<T> {
        private readonly ILogger<T> _logger;

        public LoggerAdapter (ILogger<T> logger) {
            _logger = logger;
        }

        public void LogInformation (string message) {
            _logger.LogInformation (message);
        }

        public void LogError (string message) {
            _logger.LogError (message);
        }

        public void LogWarning (string message) {
            _logger.LogWarning (message);
        }
    }
}