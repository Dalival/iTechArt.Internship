using System;
using Microsoft.Extensions.Logging;

namespace ITechArt.Common.Logger
{
    public class CustomLogger : ICustomLogger
    {
        private readonly ILogger _logger;


        public CustomLogger(ILogger logger)
        {
            _logger = logger;
        }


        public void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            var microsoftLogLevel = ConvertLogLevel(logLevel);
            _logger.Log(microsoftLogLevel, exception, message, args);
        }


        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            var microsoftLogLevel = ConvertLogLevel(logLevel);
            _logger.Log(microsoftLogLevel, message, args);
        }


        private Microsoft.Extensions.Logging.LogLevel ConvertLogLevel(LogLevel sourceLevel)
        {
            var destinationLevel = sourceLevel switch
            {
                LogLevel.Trace => Microsoft.Extensions.Logging.LogLevel.Trace,
                LogLevel.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
                LogLevel.Information => Microsoft.Extensions.Logging.LogLevel.Information,
                LogLevel.Warning => Microsoft.Extensions.Logging.LogLevel.Warning,
                LogLevel.Error => Microsoft.Extensions.Logging.LogLevel.Error,
                LogLevel.Critical => Microsoft.Extensions.Logging.LogLevel.Critical,
                _ => throw new ArgumentOutOfRangeException(nameof(sourceLevel), sourceLevel, null)
            };

            return destinationLevel;
        }
    }
}
