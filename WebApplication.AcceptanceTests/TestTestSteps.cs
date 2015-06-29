namespace WebApplication.AcceptanceTests
{
    using System.Net;
    using System.Net.Http;
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public class TestTestSteps
    {
        HttpStatusCode statusCode;

        [Given(@"the application is running")]
        public void GivenTheApplicationIsRunning()
        {
            var client = GetClient();
            // Smoke test
            client.GetAsync("").Result.EnsureSuccessStatusCode();
        }

        [When(@"I navigate to '(.*)'")]
        public void WhenINavigateTo(string resource)
        {
            var client = GetClient();
            var responseMessage = client.GetAsync(resource).Result;
            statusCode = responseMessage.StatusCode;
        }

        [Then(@"I should get (.*)")]
        public void ThenIShouldGet(HttpStatusCode expected)
        {
            statusCode.Should().Be(expected);
        }

        static HttpClient GetClient()
        {
            return Environment.GetClient();
        }
    }
}
