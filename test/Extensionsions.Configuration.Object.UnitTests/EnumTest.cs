using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests
{
    public class EnumTest
    {
        public enum TestEnum
        {
            Undefined = 0,
            Foo,
            Bar
        }

        public class TestOptions
        {
            public static string Test => "Test";

            public TestEnum[] Property { get; set; }
        }

        [Theory, AutoData]
        public void AddObject_WithEnum_ShoulBeSameInOptions(TestOptions expected)
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddObject(expected)
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<TestOptions>(configuration)
                .BuildServiceProvider();

            // Act
            var actual = serviceProvider.GetRequiredService<IOptions<TestOptions>>().Value;

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
