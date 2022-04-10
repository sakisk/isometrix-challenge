using FluentAssertions;
using Isometrix;
using Xunit;

namespace IsometrixTests;

public class LinkedListTests
{
    [Fact]
    public void ShouldCreateEmptyWithNullFirstNodeAReferenceType()
    {
        new LinkedList<TestType>().First.Should().Be(null);
    }

    [Fact]
    public void ShouldCreateEmptyWithNullFirstNodeAValueType()
    {
        new LinkedList<int>().First.Should().Be(null);
    }

    [Fact]
    public void ShouldAddFirstNode()
    {
        const int expected = 1;
        var sut = new LinkedList<int>();
        
        sut.AddFirst(expected);

        sut.First!.Data.Should().Be(expected);
    }

    private class TestType
    {
    }
}