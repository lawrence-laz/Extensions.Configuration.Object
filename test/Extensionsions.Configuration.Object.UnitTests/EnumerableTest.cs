using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests
{
    public class EnumerableTest
    {
        [Theory, AutoData]
        public void AddObject_WithEnumerableString_ShouldLoadFieldsIntoConfiguration(IEnumerable<string> expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddObject(new { MyEnumerable = expected })
                .Build();

            foreach ((var expectedItem, var index) in expected.Select((item, index) => (item, index)))
            {
                configuration[$"MyEnumerable:{index}"].Should().Be(expectedItem);
            }
        }

        [Theory, AutoData]
        public void AddObject_WithArrayList_ShouldLoadFieldsIntoConfiguration(
            string expectedString,
            int expectedInt,
            float expectedFloat)
        {
            var expected = new ArrayList
            { 
                expectedString,
                expectedInt,
                expectedFloat
            };

            var configuration = new ConfigurationBuilder()
                .AddObject(new { MyEnumerable = expected })
                .Build();

            var index = 0;
            foreach (var expectedItem in expected)
            {
                configuration[$"MyEnumerable:{index}"].Should().Be(expectedItem.ToString());
                
                ++index;
            }
        }
    }
}
