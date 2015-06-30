namespace WebApplication.AcceptanceTests
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using Properties;
    using TechTalk.SpecFlow;

    [Binding]
    public static class Environment
    {
        static WebApplication application;

        public static HttpClient GetClient()
        {
            return application.GetClient();
        }

        [BeforeTestRun]
        static void StartServer()
        {
            application = new WebApplication();
            application.Start();
            
            WaitForTheServerToStart();
        }

        [AfterTestRun]
        static void StopServer()
        {
            application?.Dispose();
        }

        static void WaitForTheServerToStart()
        {
            if (string.Equals(System.Environment.GetEnvironmentVariable("TestEnvironment"), "CI", StringComparison.Ordinal))
            {
                WaitForContinuousIntegrationEnvironment();
            }
        }

        static void WaitForContinuousIntegrationEnvironment()
        {
            var timeout = Settings.Default.ContinuousIntegrationEnvironmentSetupTimeout;
            Console.WriteLine($"Pausing for {timeout.TotalSeconds} seconds to allow the server to start.");
            Thread.Sleep(timeout);
            Console.WriteLine("Continuing with the test run.");
        }
    }
}