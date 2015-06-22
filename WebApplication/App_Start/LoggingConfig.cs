namespace WebApplication
{
    using Logging;
    using Serilog;

    public static class LoggingConfig
    {
        public static void Initialize()
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();

            LogProvider.GetCurrentClassLogger().Info("Logging initialized.");

            // TODO integrate this into Web API and MVC pipelines.
        }
    }
}