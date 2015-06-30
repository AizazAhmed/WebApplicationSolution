namespace WebApplication.AcceptanceTests
{
    using System;
    using System.IO;
    using System.Net.Http;
    using IISExpressBootstrapper;
    using Properties;

    class WebApplication : IDisposable
    {
        readonly PathParameters parameters;
        IDisposable host;

        public WebApplication()
            : this(FullPath, Settings.Default.Port)
        { }

        public WebApplication(string path, int port)
        {
            parameters = new PathParameters { Path = path, Port = port};
        }

        static string FullPath => Path.GetFullPath(Settings.Default.PhysicalPath);

        public void Start()
        {
            host = IISExpressHost.Start(parameters);
        }

        public HttpClient GetClient()
        {
            var client = new HttpClient { BaseAddress = new Uri($"http://localhost:{parameters.Port}") };
            return client;
        }

        public void Dispose()
        {
            try
            {
                host?.Dispose();
            }
            catch (InvalidOperationException ex) when (ex.Message.StartsWith("Cannot process request because the process"))
            {
                // Do nothing
            }
        }
    }
}
