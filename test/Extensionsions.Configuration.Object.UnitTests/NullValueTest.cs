using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests
{
    public class NullValueTest
    {
        class TestOptions
        {
            public object Actual { get; set; }
        }

        [Fact]
        public void AddObject_WithFieldNullValue_ShouldReturnNull()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddObject(new TestOptions { Actual = null })
                .Build();

            // Act
            var actual = configuration["Actual"];

            // Assert
            actual.Should().BeNull();
        }
    }
}
