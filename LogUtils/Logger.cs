using Serilog;

namespace FilenFolderManager.LogUtils
{
    internal class Logger
    {
        private static readonly Logger Instance = new Logger();
        private readonly ILogger _logger;

        private Logger()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void Info(string messsage)
        {
            Instance._logger.Information(messsage);
        }

    }
}
