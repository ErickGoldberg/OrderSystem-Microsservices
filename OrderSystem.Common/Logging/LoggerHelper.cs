using Microsoft.Extensions.Logging;

namespace OrderSystem.Common.Logging
{
    public static class LoggerHelper
    {
        public static void LogError(ILogger logger, Exception ex, string message)
        {
            logger.LogError(ex, message);
        }

        public static void LogInfo(ILogger logger, string message)
        {
            logger.LogInformation(message);
        }

        public static void LogWarning(ILogger logger, string message)
        {
            logger.LogWarning(message);
        }
    }
}
