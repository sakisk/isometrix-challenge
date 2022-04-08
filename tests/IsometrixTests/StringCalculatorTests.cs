using FluentAssertions;
using Isometrix;
using Xunit;

namespace IsometrixTests;

public class StringCalculatorTests
{
    [Fact]
    public void ShouldReturnZeroWhenAddingEmptyString()
    {
        new StringCalculator().Add("").Should().Be(0);
    }
}