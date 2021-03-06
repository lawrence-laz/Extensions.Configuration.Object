using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests
{
    public class ClassObjectWithPropertiesTest
    {
        internal class MyConfiguration
        {
            public PositionConfiguration Position { get; set; }
            public string MyKey { get; set; }
            public LoggingConfiguration Logging { get; set; }
            public string AllowedHosts { get; set; }
        }

        internal class PositionConfiguration
        {
            public string Title;
            public string Name;
            public int Age;
        }

        internal class LoggingConfiguration
        {
            public LogLevelConfiguration LogLevel;
        }

        internal class LogLevelConfiguration
        {
            public string Default { get; set; }
            public string Microsoft { get; set; }
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
