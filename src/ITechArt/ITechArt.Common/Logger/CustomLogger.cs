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


        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            _logger.Log(logLevel, exception, message, args);
        }

        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(Exception exception, string message, params object[] args)
        {
            _logger.LogCritical(exception, message, args);
        }

        public void LogCritical(string message, params object[] args)
        {
            _logger.LogCritical(message, args);
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            _logger.LogError(exception, message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public void LogWarning(Exception exception, string message, params object[] args)
        {
            _logger.LogWarning(exception, message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogInformation(Exception exception, string message, params object[] args)
        {
            _logger.LogInformation(exception, message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogDebug(Exception exception, string message, params object[] args)
        {
            _logger.LogDebug(exception, message, args);
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }

        public void LogTrace(Exception exception, string message, params object[] args)
        {
            _logger.LogTrace(exception, message, args);
        }

        public void LogTrace(string message, params object[] args)
        {
            _logger.LogTrace(message, args);
        }
    }
}
