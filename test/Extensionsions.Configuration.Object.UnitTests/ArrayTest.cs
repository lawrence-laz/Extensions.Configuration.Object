using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests
{
    public class ArrayTest
    {
        [Theory, AutoData]
        public void AddObject_WithStringArray_ShouldLoadFieldsIntoConfiguration(string[] expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddObject(new { MyArray = expected })
                .Build();

            foreach ((var expectedItem, var index) in expected.Select((item, index) => (item, index)))
            {
                configuration[$"MyArray:{index}"].Should().Be(expectedItem.ToString());
            }
        }
    }
}
