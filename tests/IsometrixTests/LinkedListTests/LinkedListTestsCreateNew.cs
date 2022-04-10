using FluentAssertions;
using Xunit;

namespace IsometrixTests.LinkedListTests;

public class LinkedListTestsCreateNew
{
    [Fact]
    public void ShouldCreateEmptyWithNullFirstAndLastNodeAReferenceType()
    {
        var sut = new Isometrix.LinkedList<TestType>();
        
        sut.First.Should().Be(null);
        sut.Last.Should().Be(null);
    }

    [Fact]
    public void ShouldCreateEmptyWithNullFirstAndLastNodeAValueType()
    {
        var sut = new Isometrix.LinkedList<int>();
        
        sut.First.Should().Be(null);
        sut.Last.Should().Be(null);
    }
    
    private class TestType
    {
    }
}