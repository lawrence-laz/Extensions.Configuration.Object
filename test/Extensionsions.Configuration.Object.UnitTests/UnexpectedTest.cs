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
    }
}
