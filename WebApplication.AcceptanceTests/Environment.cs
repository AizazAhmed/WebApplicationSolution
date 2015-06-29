namespace WebApplication.AcceptanceTests
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
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
            try
            {
                BrowseToBaseAddress().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static Task BrowseToBaseAddress()
        {
            var client = GetClient();
            client.Timeout = Settings.Default.EnvironmentSetupTimeout;
            return client.GetAsync("");
        }
    }
}