namespace WebApplication.AcceptanceTests
{
    using System;
    using System.Net.Http;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using IISExpressBootstrapper;
    using Properties;

    class WebApplication : IDisposable
    {
        readonly ConfigFileParameters parameters;
        IDisposable host;
        string baseAddress;

        public WebApplication()
            : this(Settings.Default.WebConfigFilePath, Settings.Default.SiteName)
        { }

        public WebApplication(string configFile, string siteName)
        {
            parameters = new ConfigFileParameters {ConfigFile = configFile, SiteName = siteName};
        }

        public void Start()
        {
            host = IISExpressHost.Start(parameters);
        }

        public HttpClient GetClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(GetBaseAddress()) };
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

        string GetBaseAddress()
        {
            if (baseAddress == null)
            {
                baseAddress = GetBaseAddressCore();
            }
            return baseAddress;
        }

        string GetBaseAddressCore()
        {
            using (var reader = XmlReader.Create(parameters.ConfigFile))
            {
                var doc = XDocument.Load(reader);
                var bindingInfo = doc.XPathEvaluate("string(/configuration/system.applicationHost/sites/site" +
                                                    $"[@name='{parameters.SiteName}']/bindings/binding[@protocol='http']" +
                                                    "/@bindingInformation)") as string;
                var parts = bindingInfo?.Split(':') ?? new string[3];
                return $"http://{parts[2]}:{parts[1]}";
            }
        }
    }
}