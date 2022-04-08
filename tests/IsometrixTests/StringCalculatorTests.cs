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
    
    [Theory]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("2", 2)]
    public void ShouldReturnNumberWhenAddingSingleNumber(string numbers, int expected)
    {
        new StringCalculator().Add(numbers).Should().Be(expected);
    }
    
    [Fact]
    public void SHouldReturnSumWhenAddingTwoNumbers()
    {
        new StringCalculator().Add("1,2").Should().Be(3);
    }
    
}