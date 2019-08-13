using System;
using System.Collections.Generic;
using System.Linq;
using ApiBuildDemo.Core.Interfases;

namespace ApiBuildDemo.Core.Services {
    public class ValueService : IValueService {
        private readonly ILoggerAdapter<ValueService> _logger;

        public ValueService (ILoggerAdapter<ValueService> logger) {
            _logger = logger;
        }

        public List<string> GetValues () {
            _logger.LogInformation ("Return values");
            return new List<string> {
                "value1",
                "value2"
            };
        }
    }
}