using System;

namespace ITechArt.Common.Logger
{
    public static class CustomLoggerExtension
    {
        public static void LogCritical(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            _logger.LogCritical(exception, message, args);
        }

        public static void LogCritical(this ICustomLogger logger, string message, params object[] args)
        {
            _logger.LogCritical(message, args);
        }

        public static void LogError(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            _logger.LogError(exception, message, args);
        }

        public static void LogError(this ICustomLogger logger, string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public static void LogWarning(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            _logger.LogWarning(exception, message, args);
        }

        public static void LogWarning(this ICustomLogger logger, string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public static void LogInformation(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            _logger.LogInformation(exception, message, args);
        }

        public static void LogInformation(this ICustomLogger logger, string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public static void LogDebug(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            _logger.LogDebug(exception, message, args);
        }

        public static void LogDebug(this ICustomLogger logger, string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }

        public static void LogTrace(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            _logger.LogTrace(exception, message, args);
        }

        public static void LogTrace(this ICustomLogger logger, string message, params object[] args)
        {
            _logger.LogTrace(message, args);
        }
    }
}
