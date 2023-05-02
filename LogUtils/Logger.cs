using Serilog;
using Serilog.Context;

namespace FilenFolderManager.LogUtils
{
    internal class Logger
    {
        private static readonly LoggerConfiguration _configuration = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.File(
                "Logs/log.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}"
            );
        
        private static readonly ILogger _logger = _configuration.CreateLogger();

        public static void Info(string message)
        {
            var className = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().DeclaringType.Name;
            var methodName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name;

            using (LogContext.PushProperty("SourceContext", $"{className}.{methodName}"))
            {
                _logger.Information(message);
            }
        }

    }
}
