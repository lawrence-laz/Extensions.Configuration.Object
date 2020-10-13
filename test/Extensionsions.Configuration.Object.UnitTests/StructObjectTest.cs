using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests
{
    public class StructObjectTest
    {
        internal struct MyConfiguration
        {
            public PositionConfiguration Position;
            public string MyKey;
            public LoggingConfiguration Logging;
            public string AllowedHosts;
        }

        internal struct PositionConfiguration
        {
            public string Title;
            public string Name;
            public int Age;
        }

        internal struct LoggingConfiguration
        {
            public LogLevelConfiguration LogLevel;
        }

        internal struct LogLevelConfiguration
        {
            public string Default;
            public string Microsoft;
        }

        [Fact]
        public void AddObject_WithAnonymousObject_ShouldLoadFieldsIntoConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddObject(new MyConfiguration
                {
                    Position = new PositionConfiguration
                    {
                        Title = "Editor",
                        Name = "Joe Smith",
                        Age = 33
                    },
                    MyKey = "My appsettings.json Value",
                    Logging = new LoggingConfiguration
                    {
                        LogLevel = new LogLevelConfiguration
                        {
                            Default = "Information",
                            Microsoft = "Warning"
                        }
                    },
                    AllowedHosts = "*"
                })
                .Build();

            configuration["Position:Title"].Should().Be("Editor");
            configuration["Position:Name"].Should().Be("Joe Smith");
            configuration["Position:Age"].Should().Be("33");
            configuration["MyKey"].Should().Be("My appsettings.json Value");
            configuration["Logging:LogLevel:Default"].Should().Be("Information");
            configuration["Logging:LogLevel:Microsoft"].Should().Be("Warning");
            configuration["AllowedHosts"].Should().Be("*");
        }
    }
}
