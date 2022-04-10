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
    
    private class TestType { }
}