using System;
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
    [InlineData("1000,2", 1002)]
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
    
    [Theory]
    [InlineData("1\n2,3", 6)]
    [InlineData("1,2\n3", 6)]
    [InlineData("1\n2\n3", 6)]
    public void ShouldReturnSumWhenAddingNumbersWithNewlines(string numbers, int expected)
    {
        new StringCalculator().Add(numbers).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("//;\n1;2", 3)]
    [InlineData("// \n1 2", 3)]
    [InlineData("//|\n1|2", 3)]
    [InlineData("//|\n1|2,3\n4", 10)]
    public void ShouldFirstLineContainDelimiterDefinition(string numbers, int expected)
    {
        new StringCalculator().Add(numbers).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("-1", "Negatives not allowed: -1")]
    [InlineData("-1,-2", "Negatives not allowed: -1,-2")]
    [InlineData("-1,2", "Negatives not allowed: -1")]
    public void ShouldThrowWhenAddingNegativeNumber(string numbers, string expected)
    {
        var addNegative = () => new StringCalculator().Add(numbers);
        addNegative.Should().Throw<InvalidOperationException>(because: expected);
    }
    
    [Theory]
    [InlineData("1001,2", 2)]
    [InlineData("2000,2", 2)]
    [InlineData("1001,2000", 0)]
    public void ShouldIgnoreNumbersBiggerThan1000(string numbers, int expected)
    {
        new StringCalculator().Add(numbers).Should().Be(expected);
    }
}