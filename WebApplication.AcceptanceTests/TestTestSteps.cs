namespace WebApplication.AcceptanceTests
{
    using System.Net;
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public class TestTestSteps
    {
        WebApplication application;
        HttpStatusCode statusCode;

        [Given(@"the application is running")]
        public void GivenTheApplicationIsRunning()
        {
            application = new WebApplication();
            application.Start();
        }

        [When(@"I navigate to '(.*)'")]
        public void WhenINavigateTo(string resource)
        {
            var client = application.GetClient();
            var responseMessage = client.GetAsync(resource).Result;
            statusCode = responseMessage.StatusCode;
        }

        [Then(@"I should get (.*)")]
        public void ThenIShouldGet(HttpStatusCode expected)
        {
            statusCode.Should().Be(expected);
        }

        [AfterScenario]
        public void CleanUp()
        {
            application?.Dispose();
        }
    }
}
