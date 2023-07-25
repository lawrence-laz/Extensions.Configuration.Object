using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests;

public class TimeSpanTest
{
    [Theory, AutoData]
    public void AddObject_WithTimeSpan_ShouldLoadFieldsIntoConfiguration(TimeSpan expected)
    {
        var configuration = new ConfigurationBuilder()
            .AddObject(new { MyTimeSpan = expected })
            .Build();

        configuration[$"MyTimeSpan"].Should().Be(expected.ToString());
    }
}
