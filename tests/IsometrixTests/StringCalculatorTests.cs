using System.Runtime.InteropServices;
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
    
    [Theory]
    [InlineData("1,1", 2)]
    [InlineData("1,2", 3)]
    public void ShouldReturnSumWhenAddingTwoNumbers(string numbers, int expected)
    {
        new StringCalculator().Add(numbers).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("1,2,3", 6)]
    [InlineData("0,5,10", 15)]
    public void ShouldReturnSumWhenAddingMultipleNumbers(string numbers, int expected)
    {
        new StringCalculator().Add(numbers).Should().Be(expected);
    }
    
}