using System;

namespace ITechArt.Common.Logger
{
    public interface ICustomLogger
    {
        bool IsEnabled(LogLevel logLevel);

        void Log<TState>(LogLevel logLevel, TState state, Exception exception,
            Func<TState, Exception, String> formatter);

        void Log(LogLevel logLevel, Exception exception, string message, params object[] args);

        void Log(LogLevel logLevel, string message, params object[] args);

        void LogFatal(Exception exception, string message, params object[] args);

        void LogFatal(string message, params object[] args);

        void LogError(Exception exception, string message, params object[] args);

        void LogError(string message, params object[] args);

        void LogWarn(Exception exception, string message, params object[] args);

        void LogWarn(string message, params object[] args);

        void LogInfo(Exception exception, string message, params object[] args);

        void LogInfo(string message, params object[] args);

        void LogDebug(Exception exception, string message, params object[] args);

        void LogDebug(string message, params object[] args);

        void LogTrace(Exception exception, string message, params object[] args);

        void LogTrace(string message, params object[] args);
    }
}
