using Microsoft.Extensions.Logging;

namespace Utilities.Logging
{
    public static class LoggerHelper
    {
        public static void LogError<T>(ILogger<T> logger, Exception ex, string message)
        {
            logger.LogError(ex, $"{typeof(T).Name} - {message}");
        }

        public static void LogInfo<T>(ILogger<T> logger, string message)
        {
            logger.LogInformation($"{typeof(T).Name} - {message}");
        }

        public static void LogWarning<T>(ILogger<T> logger, string message)
        {
            logger.LogWarning($"{typeof(T).Name} - {message}");
        }

        public static void LogDebug<T>(ILogger<T> logger, string message)
        {
            logger.LogDebug($"{typeof(T).Name} - {message}");
        }

        public static void LogCritical<T>(ILogger<T> logger, Exception ex, string message)
        {
            logger.LogCritical(ex, $"{typeof(T).Name} - {message}");
        }

        public static void LogCustom<T>(ILogger<T> logger, LogLevel level, string message, Exception? ex = null)
        {
            if (ex != null)
                logger.Log(level, ex, $"{typeof(T).Name} - {message}");
            else
                logger.Log(level, $"{typeof(T).Name} - {message}");
        }
    }
}