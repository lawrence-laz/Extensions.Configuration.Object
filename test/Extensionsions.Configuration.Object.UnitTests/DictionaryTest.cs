using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests
{
    public class DictionaryTest
    {
        [Theory, AutoData]
        public void AddObject_WithDictionary_ShouldLoadFieldsIntoConfiguration(Dictionary<string, string> expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddObject(new { MyDictionary = expected })
                .Build();

            foreach (var expectedItem in expected)
            {
                configuration[$"MyDictionary:{expectedItem.Key}"].Should().Be(expectedItem.Value);
            }
        }
    }
}
