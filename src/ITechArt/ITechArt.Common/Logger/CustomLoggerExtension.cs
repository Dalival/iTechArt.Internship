using System;

namespace ITechArt.Common.Logger
{
    public static class CustomLoggerExtension
    {
        public static void LogCritical(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            logger.Log(LogLevel.Critical, exception, message, args);
        }

        public static void LogCritical(this ICustomLogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, null, message, args);
        }

        public static void LogError(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            logger.Log(LogLevel.Error, exception, message, args);
        }

        public static void LogError(this ICustomLogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, null, message, args);
        }

        public static void LogWarning(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            logger.Log(LogLevel.Warning, exception, message, args);
        }

        public static void LogWarning(this ICustomLogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, null, message, args);
        }

        public static void LogInformation(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            logger.Log(LogLevel.Information, exception, message, args);
        }

        public static void LogInformation(this ICustomLogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, null, message, args);
        }

        public static void LogDebug(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            logger.Log(LogLevel.Debug, exception, message, args);
        }

        public static void LogDebug(this ICustomLogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, null, message, args);
        }

        public static void LogTrace(this ICustomLogger logger, Exception exception, string message,
            params object[] args)
        {
            logger.Log(LogLevel.Trace, exception, message, args);
        }

        public static void LogTrace(this ICustomLogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, null, message, args);
        }
    }
}
