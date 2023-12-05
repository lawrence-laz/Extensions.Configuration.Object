using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests
{
    public class RecordObjectTest
    {
        internal record MyConfiguration
        {
            public SubRecordConfiguration SubRecord;
            public string MyKey;
        }

        internal record SubRecordConfiguration
        {
            public string Title;
            public string Name;
            public int Age;
        }

        [Fact]
        public void AddObject_WithClassObject_ShouldLoadFieldsIntoConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddObject(new MyConfiguration
                {
                    SubRecord = new SubRecordConfiguration
                    {
                        Title = "Editor",
                        Name = "Joe Smith",
                        Age = 33
                    },
                    MyKey = "My appsettings.json Value",
                })
                .Build();

            configuration["SubRecord:Title"].Should().Be("Editor");
            configuration["SubRecord:Name"].Should().Be("Joe Smith");
            configuration["SubRecord:Age"].Should().Be("33");
            configuration["MyKey"].Should().Be("My appsettings.json Value");
        }
    }
}
