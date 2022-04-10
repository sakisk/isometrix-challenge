using FluentAssertions;
using Xunit;

namespace IsometrixTests.LinkedListTests;

public class LinkedListTestsDeletingNodes
{
    [Fact]
    public void ShouldRemoveFirstNodeInAnEmptyList()
    {
        var sut = new LinkedListBuilder<int>().Build();

        sut.RemoveFirst();

        sut.First.Should().BeNull();
    }

    [Fact]
    public void ShouldRemoveFirstNodeInAListWithOneNode()
    {
        var sut = new LinkedListBuilder<int>().WithElementsFromStart(1).Build();

        sut.RemoveFirst();

        sut.First.Should().BeNull();
    }

    [Fact]
    public void ShouldRemoveFirstNodeInAListWithMoreThanOneNode()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        
        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();
        
        sut.RemoveFirst();
        
        sut.First!.Data.Should().Be(second);
        sut.First!.Previous.Should().BeNull();
        sut.First.Next!.Data.Should().Be(third);
    }

    [Fact]
    public void ShouldRemoveLastInAnEmptyList()
    {
        var sut = new LinkedListBuilder<int>().Build();

        sut.RemoveLast();

        sut.Last.Should().BeNull();
    }
}