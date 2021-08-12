using System;

namespace ITechArt.Common.Logger
{
    public interface ICustomLogger
    {
        void Log(
            LogLevel logLevel,
            Exception exception,
            string message,
            params object[] args);
    }
}
