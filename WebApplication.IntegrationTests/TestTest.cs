namespace WebApplication.IntegrationTests
{
    using FluentAssertions;
    using Xunit;

    public class TestTest
    {
        [Fact]
        public void Test()
        {
            1.Should().Be(1);
        }
    }
}
