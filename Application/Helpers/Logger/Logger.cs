using Serilog;

namespace Application.Helpers.Logger
{
    public static class Logger
    {
        private static ILogger _logger;

        public static void InitializeLogger(string logPathFile)
        {

            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(logPathFile, rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();

            _logger.Information("Logger initialized..... logs in: " + logPathFile);

        }

        public static void LogInformation(string message, params object[] properties)
        {
            _logger.Information(message, properties);
        }

        public static void LogWarning(string message, params object[] properties)
        {
            _logger.Warning(message, properties);
        }

        public static void LogError(Exception ex, string message, params object[] properties)
        {
            _logger.Error(ex, message, properties);
        }
    }
}
