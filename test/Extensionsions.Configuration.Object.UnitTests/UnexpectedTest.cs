using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace Extensions.Configuration.Object.UnitTests
{
    public class UnexpectedTest
    {
        [Fact]
        public void AddObject_WithNull_ShouldThrow()
        {
            var sut = new ConfigurationBuilder();

            sut.Invoking(x => x.AddObject(null))
                .Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void AddObject_OnNullConfigurationBuilder_ShouldThrow()
        {
            ConfigurationBuilder sut = default;

            sut.Invoking(x => x.AddObject(default))
                .Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Build_WithConfigurationObjectNotSet_ShouldThrow()
        {
            var sut = new ObjectConfigurationSource();

            sut.Invoking(x => x.Build(default))
                .Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
