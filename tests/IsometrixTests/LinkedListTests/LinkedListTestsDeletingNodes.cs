using System;
using FluentAssertions;
using FluentAssertions.Execution;
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

        using var _ = new AssertionScope();
        sut.First.Should().BeNull();
        sut.Last.Should().BeNull();
    }

    [Fact]
    public void ShouldRemoveLastInAListWithOneNode()
    {
        var sut = new LinkedListBuilder<int>().WithElementsFromStart(1).Build();

        sut.RemoveLast();

        using var _ = new AssertionScope();
        sut.First.Should().BeNull();
        sut.Last.Should().BeNull();
    }

    [Fact]
    public void ShouldRemoveLastInAListWithMoreThanOneNode()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();

        sut.RemoveLast();

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(first);
        sut.First!.Next.Should().NotBeNull();
        sut.Last!.Data.Should().Be(second);
        sut.Last!.Next.Should().BeNull();
    }

    [Fact]
    public void ShouldRemoveElementWhenExistsInFirstNode()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        
        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();
        
        sut.RemoveElement(first);
        
        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(second);
        sut.First!.Previous.Should().BeNull();
        sut.Last!.Data.Should().Be(third);
    }
    
    [Fact]
    public void ShouldRemoveElementWhenExistsInLastNode()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        
        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();
        
        sut.RemoveElement(third);
        
        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(first);
        sut.First!.Previous.Should().BeNull();
        sut.Last!.Data.Should().Be(second);
        sut.Last!.Next.Should().BeNull();
    }
    
    [Fact]
    public void ShouldRemoveElementWhenExistsInMiddleNode()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        const int fourth = 4;
        
        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third, fourth).Build();
        
        sut.RemoveElement(second);
        
        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(first);
        sut.First!.Previous.Should().BeNull();
        sut.First!.Next!.Data.Should().Be(third);
        sut.Last!.Previous!.Data.Should().Be(third);
        sut.Last!.Data.Should().Be(fourth);
        sut.Last!.Next.Should().BeNull();
    }
    
    [Fact]
    public void ShouldThrowWhenRemovingElementDoesNotExist()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        const int fourth = 4;
        
        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third, fourth).Build();
        
        var removeNonExisting = () => sut.RemoveElement(5);
        
        removeNonExisting.Should().Throw<InvalidOperationException>().WithMessage("Element 5 not found in any node");
    }
    
}